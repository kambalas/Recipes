using ApiCommons.DTOs;

namespace RecipesUI.Services;

public interface IUserService : IApiService<UserResponse>
{
    public Task<UserResponse?> GetUserById(string id);
    public Task<UserRequest?> CreateUser(UserRequest user);
}