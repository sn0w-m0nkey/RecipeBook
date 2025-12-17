namespace RecipePdfGenerator.Models;

public class IngredientGroup
{
    public string Header { get; set; } = string.Empty;
    public List<string> Items { get; set; } = new();
}
