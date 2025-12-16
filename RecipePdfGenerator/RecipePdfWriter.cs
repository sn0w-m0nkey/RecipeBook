using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace RecipePdfGenerator
{
    public static class RecipePdfWriter
    {
        public static void GeneratePdf(Recipe recipe, string outputPath)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);

                    page.Content().Column(col =>
                    {
                        // Title
                        col.Item()
                           .PaddingBottom(10)
                           .Text(recipe.Title)
                           .FontSize(24)
                           .SemiBold()
                           .LineHeight(1.2f);

                        // Source URL
                        col.Item()
                           .PaddingBottom(20)
                           .Text(recipe.SourceUrl)
                           .FontSize(12)
                           .FontColor(Colors.Blue.Medium)
                           .LineHeight(1.2f);

                        // Ingredients
                        SectionHeading(col, "Ingredients");
                        foreach (var ingredient in recipe.Ingredients)
                        {
                            col.Item()
                               .PaddingLeft(10)
                               .PaddingBottom(2)
                               .Text($"• {ingredient}")
                               .FontSize(12)
                               .LineHeight(1.3f);
                        }

                        col.Item().PaddingBottom(10);

                        // Instructions
                        SectionHeading(col, "Instructions");
                        int step = 1;
                        foreach (var instruction in recipe.Instructions)
                        {
                            col.Item()
                               .PaddingLeft(10)
                               .PaddingBottom(2)
                               .Text($"{step}. {instruction}")
                               .FontSize(12)
                               .LineHeight(1.3f);
                            step++;
                        }

                        col.Item().PaddingBottom(10);

                        // Optional Instructions
                        AddOptionalInstructions(col, recipe.OptionalInstructions);

                        // Customizations (optional)
                        AddCustomizations(col, recipe.Customizations);
                    });
                });
            })
            .GeneratePdf(outputPath);
        }

        private static void SectionHeading(ColumnDescriptor col, string text)
        {
            col.Item()
               .PaddingBottom(5)
               .Text(text)
               .FontSize(16)
               .Bold();
        }

        private static void AddCustomizations(ColumnDescriptor col, List<string>? customizations)
        {
            if (customizations == null || customizations.Count == 0)
                return;

            SectionHeading(col, "Customizations");

            foreach (var item in customizations)
            {
                col.Item()
                   .PaddingLeft(10)
                   .PaddingBottom(2)
                   .Text($"• {item}")
                   .FontSize(12)
                   .LineHeight(1.3f);
            }
        }

        private static void AddOptionalInstructions(ColumnDescriptor col, List<string>? optionalInstructions)
        {
            if (optionalInstructions == null || optionalInstructions.Count == 0)
                return;

            SectionHeading(col, "Optional Instructions");

            foreach (var item in optionalInstructions)
            {
                col.Item()
                    .PaddingLeft(10)
                    .PaddingBottom(2)
                    .Text($"• {item}")
                    .FontSize(12)
                    .LineHeight(1.3f);
            }

            col.Item().PaddingBottom(10); // extra spacing after optional instructions
        }

    }
}
