namespace RecipesAPI.Models
{
    public class RecipeIngredient
    {
        public long RecipeId { get; set; }
        public long IngredientId { get; set; }
        public int Amount { get; set; }
    }
}
