using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using RecipesAPI.Exceptions;
using RecipesAPI.Models;
using RecipesAPI.Repositories.Interfaces;
using RecipesAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecipesAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> LogInAsync(string email, string password)
        {
            var user = await AuthenticateUserAsync(email, password);

            var token = GenerateToken(user);

            return token;
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
    }
}
