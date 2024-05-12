namespace RecipesUI.Models;

public class IngredientsRecipe
{
    public long RecipeId { get; set; }
    public long IngredientId { get; set; }
    public int Amount { get; set; }
    public MeasurementType MeasurementType { get; set; }
    
}