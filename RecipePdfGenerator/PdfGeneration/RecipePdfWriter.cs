using System.Linq;
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

                    // Servings (optional)
                    if (recipe.Servings is > 0)
                    {
                        col.Item()
                            .PaddingBottom(PdfLayout.ItemBottomPadding)
                            .Text($"Servings: {recipe.Servings}")
                            .FontSize(PdfLayout.SmallFontSize)
                            .FontColor(Colors.Grey.Darken2);
                    }

                    // Source URL
                    col.Item()
                        .PaddingBottom(PdfLayout.SectionTopPadding)
                        .Text(recipe.SourceUrl)
                        .FontSize(PdfLayout.SmallFontSize)
                        .FontColor(PdfLayout.LinkColour);

                    // INGREDIENTS
                    SectionHeading(col, "Ingredients");

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

                    // INSTRUCTIONS (supports Preparation + Cooking)
                    WriteInstructions(col, recipe);

                    // CUSTOMISATIONS
                    if (recipe.Customizations?.Any() == true)
                    {
                        SectionHeading(col, "Customisations");

                        foreach (var step in recipe.Customizations)
                            CustomBulletsOptionalOrServingBullet(col, step);
                    }

                    // OPTIONAL INSTRUCTIONS
                    if (recipe.OptionalInstructions?.Any() == true)
                    {
                        SectionHeading(col, "Optional Instructions");

                        foreach (var step in recipe.OptionalInstructions)
                            CustomBulletsOptionalOrServingBullet(col, step);
                    }

                    // SERVING SUGGESTIONS
                    if (recipe.ServingSuggestions?.Any() == true)
                    {
                        SectionHeading(col, "Serving Suggestions");

                        foreach (var suggestion in recipe.ServingSuggestions)
                            CustomBulletsOptionalOrServingBullet(col, suggestion);
                    }
                });
            });
        })
        .GeneratePdf(outputPath);
    }

    private static void WriteInstructions(ColumnDescriptor col, Recipe recipe)
    {
        var hasPrep = recipe.Preparation?.Any() == true;
        var hasCook = recipe.Cooking?.Any() == true;

        if (hasPrep || hasCook)
        {
            SectionHeading(col, "Instructions");

            if (hasPrep)
            {
                SubSectionHeading(col, "Preparation");
                for (int i = 0; i < recipe.Preparation!.Count; i++)
                    Numbered(col, i + 1, recipe.Preparation[i]);
            }

            if (hasCook)
            {
                SubSectionHeading(col, "Cooking");
                for (int i = 0; i < recipe.Cooking!.Count; i++)
                    Numbered(col, i + 1, recipe.Cooking[i]);
            }

            return;
        }

        // Backwards compatibility
        if (recipe.Instructions?.Any() == true)
        {
            SectionHeading(col, "Instructions");
            for (int i = 0; i < recipe.Instructions.Count; i++)
                Numbered(col, i + 1, recipe.Instructions[i]);
        }
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

    private static void SubSectionHeading(ColumnDescriptor col, string text)
    {
        col.Item()
            .PaddingTop(PdfLayout.GroupTopPadding)
            .PaddingBottom(PdfLayout.ItemBottomPadding)
            .Text(text)
            .FontSize(PdfLayout.GroupHeaderFontSize)
            .FontColor(Colors.Grey.Darken2)
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

    private static void CustomBulletsOptionalOrServingBullet(ColumnDescriptor col, string text)
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
