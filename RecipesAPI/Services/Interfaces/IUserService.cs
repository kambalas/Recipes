using RecipesAPI.Filters;
using RecipesAPI.Models;

namespace RecipesAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(long id);
        Task<User> CreateUserAsync(User body);
    }
}