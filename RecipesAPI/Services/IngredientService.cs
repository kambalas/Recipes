using Autofac.Extras.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Filters;
using RecipesAPI.Filters;
using RecipesAPI.Interceptor;
using RecipesAPI.Models;
using RecipesAPI.Repositories;
using RecipesAPI.Repositories.Interfaces;
using RecipesAPI.Services.Interfaces;

namespace RecipesAPI.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository recipeRepository)
        {
            _ingredientRepository = recipeRepository;
        }

        public Task<Ingredient> CreateIngredientsAsync(Ingredient body)
        {
            throw new NotImplementedException();
        }

        public Task DeleteIngredientByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientAsync(IngredientFilter filter)
        {
            var ingredientFilter = PredicateBuilder.True<Ingredient>();
            Func<IQueryable<Ingredient>, IOrderedQueryable<Ingredient>>? orderByRecipe = null;

            if (filter.Search != null)
            {
                ingredientFilter = ingredientFilter.And(x => x.Name != null && x.Name.Contains(filter.Search));
            }

            var validProperties = typeof(Recipe).GetProperties().Select(p => p.Name);
            if (validProperties.Contains(filter.OrderBy))
            {
                switch (filter.Sorting)
                {
                    case Sorting.dsc:
                        orderByRecipe = x => x.OrderByDescending(p => EF.Property<Recipe>(p, filter.OrderBy));
                        break;
                    default:
                        orderByRecipe = x => x.OrderBy(p => EF.Property<Recipe>(p, filter.OrderBy));
                        break;
                }
            }
            else
            {
                ;// filter.OrderBy value is not recognized
            }
            var recipes = await _ingredientRepository.GetAsync(
                ingredientFilter,
                orderByRecipe,
                filter.ItemsToSkip(),
                filter.PageSize
            );

            return recipes;
        }

        public Task<Ingredient> GetIngredientByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Ingredient> UpdateIngredientByIdAsync(Ingredient body, long id)
        {
            throw new NotImplementedException();
        }
    }
}
