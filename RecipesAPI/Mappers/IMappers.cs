using ApiCommons.DTOs;
using IO.Swagger.Models;
using RecipesAPI.Models;

namespace RecipesAPI.Mappers
{
    public interface IMappers
    {
        public Recipe ToRecipe(RecipeRequest recipeRequest);

        public RecipeResponse ToRecipeResponse(Recipe recipe);

        public Ingredient ToIngredient(IngredientRequest ingredientRequest);

        public IngredientResponse ToIngredientResponse(Ingredient ingredient);

        public Step ToStep(StepRequest stepRequest);

        public StepResponse ToStepResponse(Step step);
    }
}
