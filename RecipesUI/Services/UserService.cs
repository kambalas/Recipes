using ApiCommons.DTOs;

namespace RecipesUI.Services;

public class UserService : ApiService<UserResponse>, IUserService
{
    public UserService(IConfiguration configuration, ILogger<UserService> logger) : base(configuration, logger)
    {
        
    }
    
    public async Task<UserResponse?> GetUserById(string id)
    {
        try
        {
            _logger.LogInformation("Fetching user with ID: {UserId}", id);
            var response = await _httpClient.GetAsync($"user/{id}");
            response.EnsureSuccessStatusCode();

            var user = await response.Content.ReadFromJsonAsync<UserResponse>();
            _logger.LogInformation("Fetched user: {@User}", user);
            return user;
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, "HTTP error occurred while fetching user with ID: {UserId}", id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching user with ID: {UserId}", id);
            throw;
        }
    }
    
    public async Task<UserRequest?> CreateUser(UserRequest user)
    {
        try
        {
            var requestUrl = $"{_httpClient.BaseAddress}user";
            _logger.LogInformation("Creating user: {@User}", user);
            _logger.LogInformation("Request URL: {RequestUrl}", requestUrl);

            var response = await _httpClient.PostAsJsonAsync("user", user);
            response.EnsureSuccessStatusCode();

            UserRequest? createdUser = await response.Content.ReadFromJsonAsync<UserRequest>();
            _logger.LogInformation("Created user: {@CreatedUser}", createdUser);
            return createdUser;
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, "HTTP error occurred while creating user: {@User}", user);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating user: {@User}", user);
            throw;
        }
    }

}