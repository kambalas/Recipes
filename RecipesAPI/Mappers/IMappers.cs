using IO.Swagger.Models;
using RecipesAPI.Models;

namespace RecipesAPI.Mappers
{
    public interface IMappers
    {
        public Recipe ToRecipe(RecipeDTO recipeDTO);

        public RecipeDTO ToRecipeDTO(Recipe recipe);
    }
}
