using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using RecipesAPI.Exceptions;
using RecipesAPI.Models;
using RecipesAPI.Repositories.Interfaces;
using RecipesAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;


namespace RecipesAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor

        public AuthService(IUserRepository userRepository, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> LogInAsync(string email, string password)
        {
            var user = await AuthenticateUserAsync(email, password);

            var token = GenerateToken(user);

            SetTokenCookie(token);

            return token;
        }
        
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Ensure this is true in production
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(2)
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("authToken", token, cookieOptions);
        }

        private async Task<User> AuthenticateUserAsync(string email, string password)
        {

            var user = await _userRepository.GetFirstAsync(user => user.Email.ToLower() == email.ToLower());

            if (user != null)
            {
                if (user.Password.ToLower() == password.ToLower())
                    return user;
                else
                    throw new InvalidLoginCredentialsException("incorrect password");
            }
            else
            {
                throw new InvalidLoginCredentialsException("no user with this email exists");
            }
        }
        
        public void LogOut() // Method to remove the token from the cookie
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("authToken");
        }

    }
}

// OLD CODE FOR GENERATING 

// private string GenerateToken(User user)
// {
//     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
//
//     var claims = new[]
//     {
//         new Claim("UserId", user.Id.ToString()),
//     };
//
//     var token = new JwtSecurityToken(
//         _config["Jwt:Issuer"],
//         _config["Jwt:Audience"],
//         claims,
//         expires: DateTime.Now.AddHours(24),
//         signingCredentials: credentials);
//
//     return new JwtSecurityTokenHandler().WriteToken(token);
// }
