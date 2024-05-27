using IO.Swagger.Models;
using RecipesUI.Services;

public class IngredientService : ApiService<IngredientResponse>, IIngredientService
{

	public IngredientService(IConfiguration configuration, ILogger<IngredientService> logger) : base(configuration, logger)
	{
	}

	public async Task<List<IngredientResponse>> GetIngredientsAsync(string search = "")
	{
		try
		{
			if (search != "")
			{
				search = "Search=" + search;
			}
			
			var requestUri = $"{_httpClient.BaseAddress}ingredients?{search}&OrderBy=1&Sorting=asc&PageSize=50&Page=1";
			_logger.LogInformation(requestUri);
			var response = await _httpClient.GetFromJsonAsync<List<IngredientResponse>>(requestUri);
			return response ?? new List<IngredientResponse>();
		}
		catch (HttpRequestException ex)
		{
			_logger.LogError("Failed to fetch ingredients: {Message}", ex.Message);
			return new List<IngredientResponse>();
		}
	}

}
