using PoS.Application.Filters;

namespace RecipesAPI.Filters
{
    public class IngredientFilter : BaseFilter
    {
        /// <summary>
        /// Description filter for ingredients. Applies to Name. Example: "Tomato" will match recipes with "Tomato" in the Name.
        /// </summary>
        public string? Search { get; set; }
    }
}
