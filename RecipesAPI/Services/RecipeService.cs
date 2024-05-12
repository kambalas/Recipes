using Microsoft.EntityFrameworkCore;
using RecipesAPI.Filters;
using RecipesAPI.Models;
using RecipesAPI.Repositories.Interfaces;
using RecipesAPI.Services.Interfaces;

namespace PoS.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            return await _recipeRepository.InsertAsync(recipe);
        }

        public Task DeleteRecipeByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Recipe> GetRecipeByIdAsync(long id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            return recipe;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync(RecipeFilter filter)
        {
            var recipes = await _recipeRepository.GetAsync();
            return recipes;
        }

        public Task<Recipe> UpdateRecipeByIdAsync(Recipe body, long id)
        {
            throw new NotImplementedException();
        }
    }
}
