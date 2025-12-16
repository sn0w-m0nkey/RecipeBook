using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using QuestPDF.Infrastructure;

namespace RecipePdfGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set QuestPDF license to Community (free)
            QuestPDF.Settings.License = LicenseType.Community;

            string dataFolder = Path.Combine(AppContext.BaseDirectory, "Data", "Recipes");
            string outputFolder = Path.Combine(AppContext.BaseDirectory, "Output");

            // Ensure output folder exists
            Directory.CreateDirectory(outputFolder);

            // Get all JSON recipe files
            var recipeFiles = Directory.GetFiles(dataFolder, "*.json");

            var recipes = new List<Recipe>();

            foreach (var file in recipeFiles)
            {
                string json = File.ReadAllText(file);
                var recipe = JsonSerializer.Deserialize<Recipe>(json);

                if (recipe != null)
                    recipes.Add(recipe);
            }

            // Generate PDF for each recipe
            foreach (var recipe in recipes)
            {
                // Replace invalid filename characters with underscores
                string safeTitle = string.Join("_", recipe.Title.Split(Path.GetInvalidFileNameChars()));
                string outputPath = Path.Combine(outputFolder, $"{safeTitle}.pdf");

                // Generate the PDF using the static RecipePdfWriter
                RecipePdfWriter.GeneratePdf(recipe, outputPath);

                Console.WriteLine($"PDF generated: {outputPath}");
            }

            Console.WriteLine("All recipes processed!");
        }
    }
}