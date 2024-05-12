namespace RecipesUI.Models;

public class Step
{
    public long Id { get; set; }
    public long Version { get; set; }
    public string Description { get; set; }
    public StepPhase Phase { get; set; }
    public int Index { get; set; }
}