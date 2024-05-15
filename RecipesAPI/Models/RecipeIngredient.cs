using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class RecipeIngredient
    {
        [Required]
        public long? RecipeId { get; set; }
        [Required]
        public long? IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public int Amount { get; set; }
    }
}
