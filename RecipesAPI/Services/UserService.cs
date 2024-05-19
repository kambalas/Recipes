using Microsoft.EntityFrameworkCore;
using PoS.Application.Filters;
using RecipesAPI.Filters;
using RecipesAPI.Models;
using RecipesAPI.Repositories;
using RecipesAPI.Repositories.Interfaces;
using RecipesAPI.Services.Interfaces;

namespace RecipesAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> CreateUserAsync(User user)
        {
            return _userRepository.InsertAsync(user);
        }

        public Task<User> GetUserByIdAsync(long id)
        {
            return _userRepository.GetByIdAsync(id);
        }
    }
}
