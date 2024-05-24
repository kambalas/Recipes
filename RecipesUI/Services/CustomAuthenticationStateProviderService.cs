using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipesUI.Services
{
    public class CustomAuthenticationStateProviderService : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private AuthenticationState _anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public CustomAuthenticationStateProviderService(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _sessionStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrEmpty(token))
            {
                return _anonymous;
            }

            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") }, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public async Task CheckAuthenticationState()
        {
            var authState = await GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") }, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
