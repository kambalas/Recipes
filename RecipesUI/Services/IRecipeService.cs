using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCommons.DTOs;

namespace RecipesUI.Services;


public interface IRecipeService : IApiService<RecipeResponse>
{
    public Task<List<RecipeResponse>> GetRecipes(
        string search = null,
        List<int> ingredientIds = null,
        int page = 1,
        int pageSize = 9,
        string orderBy = null,
        string sorting = "asc");
    
    public Task<RecipeResponse> GetRecipeById(long id);
    public Task<bool> CreateRecipe(RecipeRequest recipe);


}