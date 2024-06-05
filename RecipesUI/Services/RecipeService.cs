using ApiCommons.DTOs;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Net;

namespace RecipesUI.Services;

public class RecipeService : ApiService<RecipeResponse>, IRecipeService
{
    
    public RecipeService(IConfiguration configuration, ILogger<RecipeService> logger) : base(configuration, logger)
    {
        
    }
    
    public async Task<List<RecipeResponse>> GetRecipes(
        string? userId =  null,
        string? search = null,
        List<int>? ingredientIds = null,
        int page = 1,
        int pageSize = 9,
        string? orderBy = null,
        string sorting = "asc")
    {
        var queryParams = new List<string>();
        
        if (!string.IsNullOrEmpty(userId))
        {
            queryParams.Add($"UserId={Uri.EscapeDataString(userId)}");
        }

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

            var recipes = JsonSerializer.Deserialize<List<RecipeResponse>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (recipes == null || !recipes.Any())
            {
                _logger.LogInformation("No recipes were found at the endpoint: {Endpoint}", endpoint);
                return new List<RecipeResponse>();
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
    
    
    
    public async Task<RecipeResponse> GetRecipeById(long id)
    {
        string endpoint = $"recipe/{id}";

        _logger.LogInformation("Fetching recipe with ID {Id} from {Endpoint}", id, endpoint);

        try
        {
            string requestUrl = $"{_httpClient.BaseAddress}{endpoint}";
            _logger.LogInformation("Request URL: {RequestUrl}", requestUrl);

            var response = await _httpClient.GetAsync(endpoint);
            var responseBody = await response.Content.ReadAsStringAsync();

            //_logger.LogInformation("Response Body: {ResponseBody}", responseBody);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogWarning("Recipe with ID {Id} was not found.", id);
                    return null;
                }

                _logger.LogError("HTTP {StatusCode} error occurred while fetching recipe with ID {Id} from {Endpoint}. Response Body: {ResponseBody}",
                                 response.StatusCode, id, endpoint, responseBody);
                throw new ApplicationException($"HTTP error occurred while fetching recipe with ID {id} from {endpoint}. Status Code: {response.StatusCode}");
            }

            var recipe = JsonSerializer.Deserialize<RecipeResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (recipe == null)
            {
                _logger.LogInformation("No recipe was found at the endpoint: {Endpoint}", endpoint);
                return null;
            }

            return recipe;
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, "HTTP error occurred while fetching recipe with ID {Id} from {Endpoint}", id, endpoint);
            throw new ApplicationException($"HTTP error occurred while fetching recipe with ID {id} from {endpoint}", httpEx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching recipe with ID {Id} from {Endpoint}", id, endpoint);
            throw new ApplicationException($"An unexpected error occurred while fetching recipe with ID {id} from {endpoint}", ex);
        }
    }
    
    public async Task<bool> CreateRecipe(RecipeRequest recipeRequest = null)
    {
    
        try
        {
            if (recipeRequest == null)
            {
                _logger.LogError("RecipeRequest is null.");
                return false;
            }
            
            _logger.LogInformation("Sending POST request to create a new recipe.");
            var response = await _httpClient.PostAsJsonAsync("recipe", recipeRequest);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Recipe created successfully.");
                return true;
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to create recipe. Status Code: {StatusCode}, Response: {Response}",
                    response.StatusCode, responseContent);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the recipe.");
            return false;
        }
    }
    
    
    public async Task<HttpStatusCode> UpdateRecipe(long id, RecipeRequest recipeRequest = null)
    {
        try
        {
            if (recipeRequest == null)
            {
                _logger.LogError("RecipeRequest is null.");
                return HttpStatusCode.BadRequest;
            }
            
            _logger.LogInformation("Sending PUT request to update recipe with ID {Id}.", id);
            var response = await _httpClient.PutAsJsonAsync($"recipe/{id}", recipeRequest);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Recipe updated successfully.");
                return response.StatusCode;
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to update recipe. Status Code: {StatusCode}, Response: {Response}",
                    response.StatusCode, responseContent);
                return response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the recipe.");
            return HttpStatusCode.InternalServerError;
        }
    }
    
    
}


    

