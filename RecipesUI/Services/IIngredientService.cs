using ApiCommons.DTOs;

namespace RecipesUI.Services;

public interface IIngredientService : IApiService<IngredientResponse>
{
    public Task<List<IngredientResponse>> GetIngredientsAsync(
        string search = "");

}