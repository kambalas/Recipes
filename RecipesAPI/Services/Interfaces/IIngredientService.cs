using RecipesAPI.Filters;
using RecipesAPI.Models;

namespace RecipesAPI.Services.Interfaces
{
    public interface IIngredientService
    {
        Task DeleteIngredientByIdAsync(long id);
        Task<Ingredient> GetIngredientByIdAsync(long id);
        Task<Ingredient> UpdateIngredientByIdAsync(Ingredient body, long id);
        Task<Ingredient> CreateIngredientsAsync(Ingredient body);
        Task<IEnumerable<Ingredient>> GetIngredientAsync(IngredientFilter filter);
    }
}
