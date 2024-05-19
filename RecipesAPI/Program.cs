using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecipesAPI.Repositories;
using RecipesAPI.Repositories.Interfaces;
using RecipesAPI.Services.Interfaces;
using RecipesAPI.Interceptor;
using RecipesAPI.Mappers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using Autofac.Extras.DynamicProxy;
using PoS.Infrastructure.Repositories;
using RecipesAPI.Services;
using RecipesAPI.Services;
using IO.Swagger.Controllers;
using Serilog;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Configuration setup
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

// Setting up Autofac
/*builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterInstance(Log.Logger).As<ILogger>();
        builder.RegisterType<AsyncLogger>().AsSelf().InstancePerDependency();

        // Register all services marked with the [Intercept] attribute
        var assembly = Assembly.GetExecutingAssembly();
        var typesToIntercept = assembly.GetTypes().Where(t => t.GetCustomAttribute<InterceptAttribute>() != null);
        foreach (var type in typesToIntercept)
        {
            builder.RegisterType(type)
                   .AsImplementedInterfaces()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(AsyncLogger))
                   .InstancePerDependency();
        }
    });
*/

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterInstance(Log.Logger).As<ILogger>();
        builder.RegisterType<AsyncLogger>().AsSelf().InstancePerDependency();

        builder.RegisterType<RecipeService>().As<IRecipeService>()
        .EnableInterfaceInterceptors()
        .InterceptedBy(typeof(AsyncLogger))
        .InstancePerDependency();

        builder.RegisterType<IngredientService>().As<IIngredientService>()
        .EnableInterfaceInterceptors()
        .InterceptedBy(typeof(AsyncLogger))
        .InstancePerDependency();


    });

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("AppDbContext")
        ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));

builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();

// Configure logging options
builder.Services.Configure<LoggingOptions>(builder.Configuration.GetSection("LoggingOptions"));

// Add repositories to the container
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

// Add mappers
builder.Services.AddScoped<IMappers, Mappers>();

// Add controllers and other middleware
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var userIdentity = context.User.Identity;

    var interceptor = app.Services.GetRequiredService<AsyncLogger>();

    interceptor.identity = userIdentity;
    await next();
});
app.MapControllers();
app.Run();
