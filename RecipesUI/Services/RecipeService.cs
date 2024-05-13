using IO.Swagger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;



namespace RecipesUI.Services;

public class RecipeService : ApiService<RecipeDTO>, IRecipeService
{
    //private readonly Logger<RecipeService> _logger;
    
    public RecipeService(IConfiguration configuration, ILogger<RecipeService> logger) : base(configuration, logger)
    {
        
    }
    
    public async Task<RecipeDTO> GetRecipes()
    {
        string _endpoint = "recipes";
        _logger.LogInformation("Fetching recipes from {Endpoint}", _endpoint);

        try
        {
            // Use GetFromJsonAsync to deserialize the JSON response directly into a RecipeDTO
            var recipe = await _httpClient.GetFromJsonAsync<RecipeDTO>(_endpoint);

            if (recipe == null)
            {
                _logger.LogInformation("No recipe was found at the endpoint: {Endpoint}", _endpoint);
                return new RecipeDTO();
            }
            return recipe;
        }
        catch (HttpRequestException httpEx)
        {
            // Log HTTP-specific exceptions, e.g., connectivity issues or HTTP response exceptions
            _logger.LogError(httpEx, "HTTP error occurred while fetching recipes from {Endpoint}", _endpoint);
            throw new ApplicationException($"HTTP error occurred while fetching recipes from {_endpoint}", httpEx);
        }
        catch (Exception ex)
        {
            // Log unexpected exceptions
            _logger.LogError(ex, "An unexpected error occurred while fetching recipes from {Endpoint}", _endpoint);
            throw new ApplicationException($"An unexpected error occurred while fetching recipes from {_endpoint}", ex);
        }
    }
    

}