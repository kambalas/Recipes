// Services/IngredientService.cs
using ApiCommons.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class IngredientService
{
	private readonly HttpClient _httpClient;

	public IngredientService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<IngredientResponse>> GetIngredientsAsync(string search = "", string orderBy = "0", string sorting = "asc", int pageSize = 0)
	{
		var response = await _httpClient.GetFromJsonAsync<List<IngredientResponse>>($"https://localhost:7087/v1/ingredients?&orderBy={orderBy}");
		return response ?? new List<IngredientResponse>();
	}
}
