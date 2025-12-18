# Recipe Project – Chat Hints

This file contains guidance for interacting with AI (ChatGPT) about your Recipe project.

## How to add a new recipe

1. Provide the **URL** of the recipe.
2. Provide the **Recipe.cs class structure** (the latest version your project uses).
3. Ask ChatGPT to generate a **JSON file** with the recipe data.
    - ChatGPT can include:
        - Title
        - SourceUrl
        - IngredientGroups (with optional headers)
        - Instructions
        - OptionalInstructions / Customizations
        - ServingSuggestions
4. Copy the JSON file into your project under `/Data/Recipes/`.

**Example prompt:**

    Please create a JSON recipe for my project.
    Recipe class structure:
    [Insert Recipe.cs here]
    Recipe URL: https://example.com/my-recipe

## Using the JSON in your project

- Static recipe classes are no longer necessary.
- The program loops through the JSON files in `/Data/Recipes/` to generate PDFs.
- Comment out any JSON files you don't want to print.

## PDF layout adjustments

- All font sizes, paddings, and line heights are controlled in `/Layout/PdfLayout.cs`.
- Section headings, group headers, bullets, and numbered instructions have descriptive constants.
- OptionalInstructions and ServingSuggestions use `OptionalOrServingBullet` with a slightly taller line height than normal ingredients for readability.

## Prompt template

- A reusable template for generating JSON recipes is in `/Data/PromptTemplate.txt`.
- You can edit this file to standardize how ChatGPT should format recipes for your project.

## Tips

- For long recipes, you can tweak:
    - `PageMargin` in `PdfLayout.cs` for more vertical space.
    - Line heights for different sections.
- Keep the naming consistent in `Recipe.cs` to maintain compatibility with `RecipePdfWriter`.
