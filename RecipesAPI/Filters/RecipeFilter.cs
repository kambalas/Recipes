using PoS.Application.Filters;

namespace RecipesAPI.Filters
{
    public class RecipeFilter : BaseFilter
    {
        public string? Search {  get; set; }

        public long[]? IngredientId { get; set; }
    }
}
