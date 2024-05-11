using IO.Swagger.Models;
using PoS.Infrastructure.Repositories;
using RecipesAPI.Repositories.Interfaces;

namespace RecipesAPI.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(AppDbContext context) : base(context) { }
    }
}