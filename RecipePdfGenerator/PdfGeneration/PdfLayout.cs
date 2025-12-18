using QuestPDF.Helpers;

namespace RecipePdfGenerator.PdfGeneration;

public static class PdfLayout
{
    // Page
    public const float PageMargin = 30;

    // Font sizes
    public const float TitleFontSize = 20;
    public const float SectionFontSize = 16;
    public const float GroupHeaderFontSize = 12;
    public const float BodyFontSize = 11;
    public const float SmallFontSize = 9;

    // Spacing
    public const float SectionTopPadding = 6;
    public const float SectionBottomPadding = 3;
    public const float ItemBottomPadding = 3;
    public const float GroupTopPadding = 4;

    // Indents
    public const float BulletIndent = 14;
    public const float NumberIndent = 22;

    // Line heights
    public const float IngredientLineHeight = 1.3f;
    public const float OptionalOrServingBulletLineHeight = 1.38f;
    public const float InstructionLineHeight = 1.45f;

    // Colours
    public static readonly string LinkColour = Colors.Blue.Medium;
}
