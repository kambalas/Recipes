using Microsoft.EntityFrameworkCore;
using PoS.Infrastructure.Repositories;
using RecipesAPI.Models;
using RecipesAPI.Repositories.Interfaces;
using System.Linq.Expressions;
using System.Linq;

namespace RecipesAPI.Repositories
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<Ingredient>> GetAsync(
       Expression<Func<Ingredient, bool>>? filter = null,
       Func<IQueryable<Ingredient>, IOrderedQueryable<Ingredient>>? orderBy = null,
       int? itemsToSkip = null,
       int? itemsToTake = null
       )
        {
            IQueryable<Ingredient> query = DbSet;

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

            return await query.ToListAsync();
        }
    }
}