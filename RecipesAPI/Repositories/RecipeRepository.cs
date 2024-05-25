using IO.Swagger.Models;
using Microsoft.EntityFrameworkCore;
using PoS.Infrastructure.Repositories;
using RecipesAPI.Models;
using RecipesAPI.Repositories.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using NuGet.Protocol.Core.Types;
using PoS.Core.Exceptions;
using System.Data.Common;

namespace RecipesAPI.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(AppDbContext context) : base(context) { }

        public override async Task<Recipe> InsertAsync(Recipe entity)
        {

            DbSet.Add(entity);
            await Context.Instance.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }


        /*        public override async Task<Recipe> UpdateAsync(Recipe updatedRecipe)
                {
                    var existingRecipe = await DbSet
                        .Include(r => r.RecipeIngredients)
                            .ThenInclude(ri => ri.Ingredient)
                        .Include(r => r.Steps)
                        .Include(r => r.User)  // Eagerly load the User
                    .FirstOrDefaultAsync(r => r.Id == updatedRecipe.Id);
                    if (existingRecipe == null)
                    {
                        throw new ApiException("Recipe not found", System.Net.HttpStatusCode.NotFound);
                    }

                    if (updatedRecipe.UserId == null)
                    {
                        updatedRecipe.UserId = existingRecipe.UserId;
                    }
                    Context.Entry(existingRecipe).CurrentValues.SetValues(updatedRecipe);

                    await Context.SaveChangesAsync();

                    return existingRecipe;
                }
        */
        public override async Task<Recipe> UpdateAsync(Recipe updatedRecipe)
        {
            var existingRecipe = await DbSet
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Steps)
                .FirstOrDefaultAsync(r => r.Id == updatedRecipe.Id);

            if (existingRecipe == null)
            {
                throw new ApiException("Recipe not found", System.Net.HttpStatusCode.NotFound);
            }

            if (updatedRecipe.UserId != null)
            {
                existingRecipe.UserId = updatedRecipe.UserId;
            }
            existingRecipe.RecipeIngredients.Clear();
            existingRecipe.Steps.Clear();

            Context.Instance.Entry(existingRecipe).CurrentValues.SetValues(updatedRecipe);

            if (updatedRecipe.RecipeIngredients != null && updatedRecipe.RecipeIngredients.Any())
            {
                updatedRecipe.RecipeIngredients.ToList().ForEach(ri => existingRecipe.RecipeIngredients.Add(ri));
            }
            if (updatedRecipe.Steps != null && updatedRecipe.Steps.Any())
            {
                updatedRecipe.Steps.ToList().ForEach(s => existingRecipe.Steps.Add(s));
            }

            Context.Instance.Entry(existingRecipe).OriginalValues["Version"] = updatedRecipe.Version;

            await Context.Instance.SaveChangesAsync();

            var updatedRecipeWithRelatedEntities = await DbSet
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Steps)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == updatedRecipe.Id);

            return updatedRecipeWithRelatedEntities;
        }





        public override async Task<Recipe> GetByIdAsync(object id)
        {
            var entity = await DbSet
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Steps)
                .Include(r => r.User)  // Eagerly load the User
                .FirstOrDefaultAsync(r => r.Id == (long)id);

            if (entity == null)
                throw new NotImplementedException();
            else
                return entity;
        }

        public async Task<Recipe> AddNewRecipe(Recipe recipe)
		{
			return await InsertAsync(recipe);
		}

        public override async Task<IEnumerable<Recipe>> GetAsync(
    Expression<Func<Recipe, bool>>? filter = null,
    Func<IQueryable<Recipe>, IOrderedQueryable<Recipe>>? orderBy = null,
    int? itemsToSkip = null,
    int? itemsToTake = null
)
        {
            IQueryable<Recipe> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (itemsToSkip != null)
            {
                query = query.Skip((int)itemsToSkip);
            }

            if (itemsToTake != null)
            {
                query = query.Take((int)itemsToTake);
            }

            return await query
                .Include(r => r.Ingredients)
                .Include(r => r.User)
                .ToListAsync();
        }

    }
}