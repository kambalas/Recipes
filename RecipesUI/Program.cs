using System.Text.Json;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using RecipesUI.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddMudServices();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
});
builder.Services.AddScoped(typeof(IApiService<>), typeof(ApiService<>));
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7087/") });
builder.Services.AddScoped<CustomAuthenticationStateProviderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProviderService>());
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};



builder.Services.AddLogging();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
