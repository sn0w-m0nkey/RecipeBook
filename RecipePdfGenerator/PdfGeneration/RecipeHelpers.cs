using QuestPDF.Fluent;

namespace RecipePdfGenerator
{
    public static class RecipeHelpers
    {
        public static void Bullet(ColumnDescriptor col, string text)
        {
            col.Item().PaddingVertical(2).Row(row =>
            {
                row.AutoItem().Text("• ").FontSize(12);
                row.RelativeItem()
                    .PaddingLeft(5)
                    .Text(text)
                    .FontSize(12);
            });
        }

        public static void Numbered(ColumnDescriptor col, int number, string text)
        {
            col.Item().PaddingVertical(4).Row(row =>
            {
                row.AutoItem().Text($"{number}. ").FontSize(12).Bold();
                row.RelativeItem()
                    .PaddingLeft(5)
                    .Text(text)
                    .FontSize(12);
            });
        }

        public static void SectionHeading(ColumnDescriptor col, string text)
        {
            col.Item().PaddingTop(8).PaddingBottom(4).Text(text).Bold().FontSize(13);
        }
    }
}