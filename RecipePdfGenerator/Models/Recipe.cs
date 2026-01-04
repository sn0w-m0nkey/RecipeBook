namespace RecipePdfGenerator.Models;

public class Recipe
{
    public string Title { get; set; } = "";
    public string SourceUrl { get; set; } = "";
    public int? Servings { get; set; }

    public List<IngredientGroup> IngredientGroups { get; set; } = new();

    public List<string> Instructions { get; set; } = new();

    public List<string>? Preparation { get; set; }
    public List<string>? Cooking { get; set; }

    public List<string>? Customizations { get; set; }
    public List<string>? OptionalInstructions { get; set; }
    public List<string>? ServingSuggestions { get; set; }
}