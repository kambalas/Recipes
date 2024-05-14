using IO.Swagger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using IO.Swagger.Models;

namespace RecipesUI.Services;


public interface IRecipeService : IApiService<RecipeDTO>
{
    public Task<RecipeDTO> GetRecipes();


}