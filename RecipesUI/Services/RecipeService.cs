using ApiCommons.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace RecipesUI.Services;

public class RecipeService : ApiService<RecipeResponse>, IRecipeService
{
    //private readonly Logger<RecipeService> _logger;
    
    public RecipeService(IConfiguration configuration, ILogger<RecipeService> logger) : base(configuration, logger)
    {
        
    }
    
    public async Task<List<RecipeResponse>> GetRecipes(
        string search = null,
        List<int> ingredientIds = null,
        int page = 1,
        int pageSize = 9,
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

            _logger.LogInformation("Response Body: {ResponseBody}", responseBody);

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
        var ingredients = new List<IngredientOnCreateRequest>();
        ingredients.Add(new IngredientOnCreateRequest()
        {
            Id = 1,
            Amount = 1,
        });
        ingredients.Add(new IngredientOnCreateRequest()
        {
            Id = 2,
            Amount = 1,
        });

        var recipeRequestTest = new RecipeRequest
        {
            Name = "Test Recipe",
            Description = "This is a test recipe",
            Ingredients = ingredients,
            /*            Ingredients = new List<IngredientRequest>
                        {
                            new IngredientRequest
                            {
                                Name = "Test Ingredient 1",
                                Measurement = MeasurementEnum.GEnum,
                                //Amount = 2
                            },
                            new IngredientRequest
                            {
                                Name = "Test Ingredient 2",
                                Measurement = MeasurementEnum.MlEnum,
                                //Amount = 3
                            }
                        },*/
            Steps = new List<StepRequest>
            {
                new StepRequest()
                {
                    StepNumber = 1,
                    Description = "This is step 1",
                    Phase = PhaseEnum.PrepEnum
                },
                new StepRequest()
                {
                    StepNumber = 2,
                    Description = "This is step 2",
                    Phase = PhaseEnum.PrepEnum }
            },
            Servings = 4,
            Energy = 500,
            Level = LevelEnum.EasyEnum
        };
        
        
        
        
        try
        {
            _logger.LogInformation("Sending POST request to create a new recipe.");
            var response = await _httpClient.PostAsJsonAsync("recipe", recipeRequestTest);

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

    
    
}


    

