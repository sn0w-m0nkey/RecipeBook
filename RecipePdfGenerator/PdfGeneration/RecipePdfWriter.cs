using QuestPDF.Fluent;
using QuestPDF.Helpers;
using RecipePdfGenerator.Models;

namespace RecipePdfGenerator.PdfGeneration;

public static class RecipePdfWriter
{
    public static void WriteRecipePdf(Recipe recipe, string outputPath)
    {
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(PdfLayout.PageMargin);

                page.Content().Column(col =>
                {
                    // Title
                    col.Item()
                        .PaddingBottom(PdfLayout.ItemBottomPadding)
                        .Text(recipe.Title)
                        .FontSize(PdfLayout.TitleFontSize)
                        .Bold();

                    // Source URL
                    col.Item()
                        .PaddingBottom(PdfLayout.SectionTopPadding)
                        .Text(recipe.SourceUrl)
                        .FontSize(PdfLayout.SmallFontSize)
                        .FontColor(PdfLayout.LinkColour);

                    // INGREDIENTS
                    SectionHeading(col, "Ingredients");

                    if (recipe.IngredientGroups != null && recipe.IngredientGroups.Any())
                    {
                        foreach (var group in recipe.IngredientGroups)
                        {
                            col.Item()
                                .PaddingTop(PdfLayout.GroupTopPadding)
                                .Text(group.Header)
                                .FontSize(PdfLayout.GroupHeaderFontSize)
                                .Bold();

                            foreach (var item in group.Items)
                                Bullet(col, item);
                        }
                    }
                    else if (recipe.Ingredients != null)
                    {
                        foreach (var ingredient in recipe.Ingredients)
                            Bullet(col, ingredient);
                    }

                    // INSTRUCTIONS
                    SectionHeading(col, "Instructions");

                    for (int i = 0; i < recipe.Instructions.Count; i++)
                        Numbered(col, i + 1, recipe.Instructions[i]);

                    // OPTIONAL INSTRUCTIONS
                    if (recipe.OptionalInstructions?.Any() == true)
                    {
                        SectionHeading(col, "Optional Instructions");

                        foreach (var step in recipe.OptionalInstructions)
                            OptionalOrServingBullet(col, step);
                    }

                    // SERVING SUGGESTIONS
                    if (recipe.ServingSuggestions?.Any() == true)
                    {
                        SectionHeading(col, "Serving Suggestions");

                        foreach (var suggestion in recipe.ServingSuggestions)
                            OptionalOrServingBullet(col, suggestion);
                    }
                });
            });
        })
        .GeneratePdf(outputPath);
    }

    private static void SectionHeading(ColumnDescriptor col, string text)
    {
        col.Item()
            .PaddingTop(PdfLayout.SectionTopPadding)
            .PaddingBottom(PdfLayout.SectionBottomPadding)
            .Text(text)
            .FontSize(PdfLayout.SectionFontSize)
            .Bold();
    }

    private static void Bullet(ColumnDescriptor col, string text)
    {
        col.Item().Row(row =>
        {
            row.ConstantItem(PdfLayout.BulletIndent)
                .Text("•")
                .FontSize(PdfLayout.BodyFontSize);

            row.RelativeItem()
                .Text(text)
                .FontSize(PdfLayout.BodyFontSize)
                .LineHeight(PdfLayout.IngredientLineHeight);
        });
    }

    private static void OptionalOrServingBullet(ColumnDescriptor col, string text)
    {
        col.Item().Row(row =>
        {
            row.ConstantItem(PdfLayout.BulletIndent)
                .Text("•")
                .FontSize(PdfLayout.BodyFontSize);

            row.RelativeItem()
                .Text(text)
                .FontSize(PdfLayout.BodyFontSize)
                .LineHeight(PdfLayout.OptionalOrServingBulletLineHeight);
        });
    }

    private static void Numbered(ColumnDescriptor col, int number, string text)
    {
        col.Item().Row(row =>
        {
            row.ConstantItem(PdfLayout.NumberIndent)
                .Text($"{number}.")
                .FontSize(PdfLayout.BodyFontSize);

            row.RelativeItem()
                .Text(text)
                .FontSize(PdfLayout.BodyFontSize)
                .LineHeight(PdfLayout.InstructionLineHeight);
        });
    }
}
