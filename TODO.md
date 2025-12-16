# Recipes – PDF Generator

A simple .NET console application that generates clean, printable PDF recipes from structured JSON data.

The project is intentionally minimal:
- No scraping
- No databases
- No UI
- Just clean data → clean PDFs

PDF generation is handled using QuestPDF.

---

## Project Structure

```
Recipes/
    RecipePdfGenerator/
        Program.cs
        Recipe.cs
        RecipePdfWriter.cs
        RecipeHelpers.cs
        Data/
            Recipes/
                TestRecipe.json
                HoneyButterChicken_HermanAtHome.json
                PromptTemplate.txt
        Output/
            *.pdf
```

---

## How It Works

1. Recipes are stored as JSON files in:
   ```
   Data/Recipes
   ```

2. On application startup:
   - All .json files are loaded
   - Each file is deserialized into a Recipe
   - One PDF is generated per recipe

3. PDFs are written to:
   ```
   Output/
   ```

---

## Running the Application

```bash
dotnet run
```

The required JSON files are automatically copied to the output directory via the project file configuration.

---

## Recipe JSON Format

Each recipe is stored as a JSON file matching the Recipe.cs structure.

Example:

```json
{
  "Title": "Honey Butter Chicken (HermanAtHome)",
  "SourceUrl": "https://hermanathome.com/honey-butter-chicken/",
  "Ingredients": [
    "680 g boneless skinless chicken thighs",
    "2 tbsp soy sauce",
    "1 tbsp honey"
  ],
  "Instructions": [
    "Mix ingredients and marinate.",
    "Cook chicken until done."
  ],
  "Customizations": [
    "Add chilli flakes for heat.",
    "Serve with vegetables."
  ]
}
```

---

## Adding a New Recipe (Manual)

1. Create a new .json file in Data/Recipes
2. Follow the existing recipe structure
3. Run the application
4. A new PDF will be generated automatically

---

## Adding a New Recipe Using ChatGPT

You can ask ChatGPT to generate a recipe JSON file for this project without requiring any prior conversation context.

Provide the following information:

1. The URL of the recipe you want added
2. The current Recipe.cs structure used by the project

You can also use the reusable prompt template located at:

```
Data/Recipes/PromptTemplate.txt
```

This template contains a ready-to-use prompt you can copy, paste, and just replace `{RECIPE_URL}` with the actual URL.

ChatGPT will return a single JSON object ready to save into Data/Recipes.

---

## Project TODOs

The project's planned improvements and future ideas are documented in:

```
TODO.md
```

This file contains:
- Core improvements (PDF layout, spacing, page breaks)
- Recipe model enhancements (servings, prep time, tags)
- Data handling improvements (validation, multiple folders, URL import)
- PDF output improvements (header/footer, layouts)
- Automation (CLI flags, batch import, CI builds)
- Long-term ideas (Web UI, database-backed recipes, EPUB/Markdown support)

Refer to TODO.md to see the full list and track progress on future work.

---

## Technologies Used

- .NET 9
- QuestPDF
- System.Text.Json

---

## Design Notes

- Recipes are stored as data, not code
- JSON files are treated like static content
- The PDF writer is recipe-agnostic
- The structure is designed to evolve without rewriting existing recipes
