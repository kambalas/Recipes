using IO.Swagger.Models;
using Microsoft.EntityFrameworkCore;
using PoS.Infrastructure.Repositories;
using RecipesAPI.Models;
using RecipesAPI.Repositories.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using System.ComponentModel.DataAnnotations;

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