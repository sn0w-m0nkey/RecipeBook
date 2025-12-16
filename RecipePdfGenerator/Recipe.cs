using System.Collections.Generic;

namespace RecipePdfGenerator
{
    public class Recipe
    {
        public string Title { get; set; } = string.Empty;
        public string SourceUrl { get; set; } = string.Empty;
        public List<string> Ingredients { get; set; } = new();
        public List<string> Instructions { get; set; } = new();
        public List<string>? Customizations { get; set; }
        public List<string>? OptionalInstructions { get; set; }
    }
}