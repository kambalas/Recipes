using Microsoft.EntityFrameworkCore;
using PoS.Application.Filters;
using RecipesAPI.Exceptions;
using RecipesAPI.Filters;
using RecipesAPI.Models;
using RecipesAPI.Repositories;
using RecipesAPI.Repositories.Interfaces;
using RecipesAPI.Services.Interfaces;
using System.Text.RegularExpressions;

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
            var emailRegex = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";

            if (Regex.IsMatch(user.Email, emailRegex))
            {
                return _userRepository.InsertAsync(user);
            }
            else
            {
                throw new InvalidEmailException();
            }
        }

        public Task<User> GetUserByIdAsync(long id)
        {
            return _userRepository.GetByIdAsync(id);
        }
    }
}
