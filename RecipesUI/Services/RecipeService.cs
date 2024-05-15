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
    
    public async Task<List<RecipeDTO>> GetRecipes(
        string search = null,
        List<int> ingredientIds = null,
        int page = 1,
        int pageSize = 10,
        string orderBy = null,
        string sorting = "asc")
    {
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(search))
        {
            queryParams.Add($"Search={Uri.EscapeDataString(search)}");
        }

        if (ingredientIds != null && ingredientIds.Any())
        {
            queryParams.AddRange(ingredientIds.Select(id => $"IngredientId={id}"));
        }

        queryParams.Add($"Page={page}");
        queryParams.Add($"PageSize={pageSize}");

        if (!string.IsNullOrEmpty(orderBy))
        {
            queryParams.Add($"OrderBy={Uri.EscapeDataString(orderBy)}");
        }

        if (!string.IsNullOrEmpty(sorting))
        {
            queryParams.Add($"Sorting={Uri.EscapeDataString(sorting)}");
        }

        string queryString = string.Join("&", queryParams);
        string endpoint = $"recipes?{queryString}";

        _logger.LogInformation("Fetching recipes from {Endpoint}", endpoint);

        try
        {
            string requestUrl = $"{_httpClient.BaseAddress}{endpoint}";
            _logger.LogInformation("Request URL: {RequestUrl}", requestUrl);

            var response = await _httpClient.GetAsync(endpoint);
            var responseBody = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("Response Body: {ResponseBody}", responseBody);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("HTTP {StatusCode} error occurred while fetching recipes from {Endpoint}. Response Body: {ResponseBody}",
                                 response.StatusCode, endpoint, responseBody);
                throw new ApplicationException($"HTTP error occurred while fetching recipes from {endpoint}. Status Code: {response.StatusCode}");
            }

            var recipes = JsonSerializer.Deserialize<List<RecipeDTO>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (recipes == null || !recipes.Any())
            {
                _logger.LogInformation("No recipes were found at the endpoint: {Endpoint}", endpoint);
                return new List<RecipeDTO>();
            }

            return recipes;
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, "HTTP error occurred while fetching recipes from {Endpoint}", endpoint);
            throw new ApplicationException($"HTTP error occurred while fetching recipes from {endpoint}", httpEx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching recipes from {Endpoint}", endpoint);
            throw new ApplicationException($"An unexpected error occurred while fetching recipes from {endpoint}", ex);
        }
    }
    
    
}


    

