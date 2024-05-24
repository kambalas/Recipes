using PoS.Application.Filters;

namespace RecipesAPI.Filters
{
    public class RecipeFilter : BaseFilter
    {

        /// <summary>
        /// Description filter for recipes. Applies to both Name and Description properties. Example: "Tomato" will match recipes with "Tomato" in either Name or Description.
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// Array of ingredient IDs to filter recipes by. Example: api/Recipes?IngredientId=1&amp;IngredientId=2 will filter recipes that contain ingredients with IDs 1 and 2.
        /// </summary>

        public int[]? IngredientId { get; set; }

        public int? UserId { get; set; }
    }

    public enum RecipeOrderBy
    {
        Name,
        CreatedAt,
        UpdatedAt,
        PreparationTimeInSeconds,
        CookingTimeInSeconds,
        Servings,
        EnergyInKCal,
        Level
    }

}
