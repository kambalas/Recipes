using Microsoft.EntityFrameworkCore;
using RecipesAPI.Repositories;
using RecipesAPI.Repositories.Interfaces;
using RecipesAPI.Services.Interfaces;
using RecipesAPI.Interceptor;
using RecipesAPI.Mappers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using PoS.Infrastructure.Repositories;
using RecipesAPI.Services;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using ILogger = Serilog.ILogger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using RecipesAPI.Models;
using RecipesAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);



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

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));
//options.UseMySQL(builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'savingsAppContext' not found.")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<LoggingOptions>(builder.Configuration.GetSection("LoggingOptions"));
// Add repositories to the container
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IMappers, Mappers>();

//builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.Decorate<IUserRepository, UserRepositoryDecorator>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.Use(async (context, next) =>
{
    var userIdentity = context.User.Identity;
    var sd = context.User.Claims;

    var interceptor = app.Services.GetRequiredService<AsyncLogger>();
    interceptor.identity = userIdentity;
    await next();
});
app.MapControllers();

app.Run();