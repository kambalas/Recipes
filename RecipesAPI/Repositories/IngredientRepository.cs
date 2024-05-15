using PoS.Infrastructure.Repositories;
using RecipesAPI.Models;
using RecipesAPI.Repositories.Interfaces;

namespace RecipesAPI.Repositories
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext context) : base(context) { }
    }
}