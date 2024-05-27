using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Authorization;
using ApiCommons.DTOs;
using Microsoft.AspNetCore.Http;
using Blazored.SessionStorage;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace RecipesUI.Services
{
    public class CustomAuthenticationStateProviderService : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CustomAuthenticationStateProviderService> _logger;

        private AuthenticationState _anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        
        public CustomAuthenticationStateProviderService(IHttpClientFactory httpClientFactory, ILogger<CustomAuthenticationStateProviderService> logger, IJSRuntime jsRuntime)
        {
            // _httpContextAccessor = httpContextAccessor
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _jsRuntime = jsRuntime;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("cookieHelper.getCookie", "authToken");

            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("No auth token found in cookies. Returning anonymous authentication state.");
                return await Task.FromResult(_anonymous);
            }

            _logger.LogInformation("Auth token found in cookies. Creating authenticated user.");
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") }, "jwt");
            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public async Task<AuthenticationState> CheckAuthenticationStateAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
            return authState;
        }

        public async Task MarkUserAsAuthenticatedAsync(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") }, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            await _jsRuntime.InvokeVoidAsync("cookieHelper.setCookie", "authToken", token, 1);
            _logger.LogInformation("Auth token cookie set successfully.");

            NotifyAuthenticationStateChanged(authState);
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            await _jsRuntime.InvokeVoidAsync("cookieHelper.eraseCookie", "authToken");
            _logger.LogInformation("Auth token cookie deleted. User logged out.");

            NotifyAuthenticationStateChanged(authState);
        }
        
        public string GetUserIdFromToken(string token)
        {
            try
            {
                var parts = token.Split('.');
                if (parts.Length != 3)
                {
                    throw new ArgumentException("Token is not in the correct format");
                }
                
                var payload = parts[1];
                var jsonBytes = ParseBase64WithoutPadding(payload);
                var jsonString = Encoding.UTF8.GetString(jsonBytes);
                var keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);


                if (keyValuePairs.TryGetValue("UserId", out var userId))
                {
                    return userId.ToString();
                }

                _logger.LogWarning("UserId claim not found in token.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error decoding JWT token.");
                throw;
            }
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        
        public async Task<string> LoginAsync(LogInRequest loginRequest)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.BaseAddress = new Uri("https://localhost:7087/v1/");
        
                _logger.LogInformation("Submitting login request to {Uri} with payload: {Payload}", httpClient.BaseAddress + "auth/login", loginRequest);

                var response = await httpClient.PostAsJsonAsync("auth/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("Login successful. Token received: {Token}", token);
                    await MarkUserAsAuthenticatedAsync(token);
                    return token;
                }

                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error logging in: {Error}. Response status: {Status}", error, response.StatusCode);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in.");
                return null;
            }
        }

        

        // public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        // {
        //     var token = _httpContextAccessor.HttpContext?.Request.Cookies["authToken"];
        //
        //     if (string.IsNullOrEmpty(token))
        //     {
        //         _logger.LogWarning("No auth token found in cookies. Returning anonymous authentication state.");
        //         return await Task.FromResult(_anonymous);
        //
        //     }
        //     
        //     _logger.LogInformation("Auth token found in cookies. Creating authenticated user.");
        //     var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") }, "jwt");
        //     var user = new ClaimsPrincipal(identity);
        //
        //     return await Task.FromResult(new AuthenticationState(user));
        // }
        //

        // public async Task<AuthenticationState> CheckAuthenticationState()
        // {
        //     var authState = await GetAuthenticationStateAsync();
        //     NotifyAuthenticationStateChanged(Task.FromResult(authState));
        //     return authState;
        // }
        //
        // public void MarkUserAsAuthenticated(string token)
        // {
        //     var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") }, "jwt"));
        //     var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        //
        //     var cookieOptions = new CookieOptions
        //     {
        //         HttpOnly = true,
        //         Secure = true, // Ensure to use HTTPS in production
        //         SameSite = SameSiteMode.Strict,
        //         Expires = DateTime.UtcNow.AddHours(1)
        //     };
        //
        //     // _httpContextAccessor.HttpContext.Response.Cookies.Append("authToken", token, cookieOptions);
        //     _httpContextAccessor.HttpContext?.Response.Cookies.Append("authToken", token, cookieOptions);
        //     _logger.LogInformation("Auth token cookie set successfully.");
        //     
        //     NotifyAuthenticationStateChanged(authState);
        // }
        //
        // public void MarkUserAsLoggedOut()
        // {
        //     var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        //     var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        //     _httpContextAccessor.HttpContext?.Response.Cookies.Delete("authToken");
        //     _logger.LogInformation("Auth token cookie deleted. User logged out.");
        //     
        //     NotifyAuthenticationStateChanged(authState);
        // }
    }
}
