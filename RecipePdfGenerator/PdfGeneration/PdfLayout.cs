using QuestPDF.Helpers;

namespace RecipePdfGenerator.PdfGeneration;

public static class PdfLayout
{
    // Page
    public const float PageMargin = 40;

    // Font sizes
    public const float TitleFontSize = 24;
    public const float SectionFontSize = 16;
    public const float GroupHeaderFontSize = 13;
    public const float BodyFontSize = 12;
    public const float SmallFontSize = 10;

    // Spacing
    public const float SectionTopPadding = 15;
    public const float SectionBottomPadding = 6;
    public const float ItemBottomPadding = 4;
    public const float GroupTopPadding = 6;

    // Indents
    public const float BulletIndent = 16;
    public const float NumberIndent = 26;

    // Line heights
    public const float BodyLineHeight = 1.35f;
    public const float InstructionLineHeight = 1.4f;

    // Colours
    public static readonly string LinkColour = Colors.Blue.Medium;
}