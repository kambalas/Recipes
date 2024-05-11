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

        public Task<Recipe> CreateRecipeAsync(Recipe body)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRecipeByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> GetRecipeByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Recipe>> GetRecipesAsync(RecipeFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> UpdateRecipeByIdAsync(Recipe body, long id)
        {
            throw new NotImplementedException();
        }
    }
}
