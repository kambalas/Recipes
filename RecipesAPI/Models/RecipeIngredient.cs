using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class RecipeIngredient
    {
        [Required]
        public long? RecipeId { get; set; }
        [Required]
        public long? IngredientId { get; set; }
        public int Amount { get; set; }
    }
}
