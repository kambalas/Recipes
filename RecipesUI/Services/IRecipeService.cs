using IO.Swagger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using IO.Swagger.Models;

namespace RecipesUI.Services;


public interface IRecipeService : IApiService<RecipeDTO>
{
    public Task<List<RecipeDTO>> GetRecipes(
        string search = null,
        List<int> ingredientIds = null,
        int page = 1,
        int pageSize = 9,
        string orderBy = null,
        string sorting = "asc");
    
    public Task<RecipeDTO> GetRecipeById(long id);


}