using RecipesAPI.Filters;
using RecipesAPI.Models;

namespace RecipesAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LogInAsync(string email, string password);
    }
}