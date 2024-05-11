using RecipesAPI.Filters;
using RecipesAPI.Models;

namespace RecipesAPI.Services.Interfaces
{
    public interface IRecipeService
    {
        Task DeleteRecipeByIdAsync(long id);
        Task<Recipe> GetRecipeByIdAsync(long id);
        Task<Recipe> UpdateRecipeByIdAsync(Recipe body, long id);
        Task<Recipe> CreateRecipeAsync(Recipe body);
        Task<IEnumerable<Recipe>> GetRecipesAsync(RecipeFilter filter);
    }
}