namespace RecipePdfGenerator.Models;

public class Recipe
{
    public string Title { get; set; } = string.Empty;
    public string SourceUrl { get; set; } = string.Empty;

    // OLD format (still supported)
    public List<string>? Ingredients { get; set; }

    // NEW format (optional)
    public List<IngredientGroup>? IngredientGroups { get; set; }

    public List<string> Instructions { get; set; } = new();
    public List<string>? Customizations { get; set; }
    public List<string>? OptionalInstructions { get; set; }
    public List<string>? ServingSuggestions { get; set; }
}
