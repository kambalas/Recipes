using Autofac.Extras.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Filters;
using RecipesAPI.Filters;
using RecipesAPI.Interceptor;
using RecipesAPI.Models;
using RecipesAPI.Repositories;
using RecipesAPI.Repositories.Interfaces;
using RecipesAPI.Services.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace RecipesAPI.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<Recipe> CreateRecipesAsync(Recipe recipe)
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
            var recipeFilter = PredicateBuilder.True<Recipe>();
            Func<IQueryable<Recipe>, IOrderedQueryable<Recipe>>? orderByRecipe = null;

            if (filter.Search != null)
            {
                recipeFilter = recipeFilter.And(
                    x => (x.Description != null && x.Description.Contains(filter.Search)) || ( x.Name != null &&  x.Name.Contains(filter.Search)));
            }

            if (filter.IngredientId != null && filter.IngredientId.Length > 0)
            {
                var ingredientIds = filter.IngredientId;
                foreach (var ingredientId in ingredientIds)
                {
                    var id = ingredientId;
                    recipeFilter = recipeFilter.And(x => x.Ingredients.Any(i => i.Id == id));
                }
            }

            if (filter.UserId != null)
            {
                recipeFilter = recipeFilter.And(x => x.User.Id == filter.UserId);
            }

            if (Enum.TryParse<RecipeOrderBy>(filter.OrderBy, out var orderByValue))
            {
                Expression<Func<Recipe, object>> orderByExpression = orderByValue switch
                {
                    RecipeOrderBy.Name => x => x.Name,
                    RecipeOrderBy.CreatedAt => x => x.CreatedAt,
                    RecipeOrderBy.UpdatedAt => x => x.UpdatedAt,
                    RecipeOrderBy.PreparationTimeInSeconds => x => x.PreparationTimeInSeconds,
                    RecipeOrderBy.CookingTimeInSeconds => x => x.CookingTimeInSeconds,
                    RecipeOrderBy.Servings => x => x.Servings,
                    RecipeOrderBy.EnergyInKCal => x => x.EnergyInKCal,
                    RecipeOrderBy.Level => x => x.Level,
                    _ => x => x.Name
                };

                orderByRecipe = filter.Sorting switch
                {
                    Sorting.dsc => x => x.OrderByDescending(orderByExpression),
                    _ => x => x.OrderBy(orderByExpression)
                };
            }

/*                var validProperties = typeof(Recipe).GetProperties().Select(p => p.Name);
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
            }*/
            var recipes = await _recipeRepository.GetAsync(
                recipeFilter,
                orderByRecipe,
                filter.ItemsToSkip(),
                filter.PageSize
            );

            return recipes;
        }

        public async Task<Recipe> UpdateRecipeByIdAsync(Recipe body, long id)
        {
            body.Id = id;
            return await _recipeRepository.UpdateAsync(body);
        }
    }
}
