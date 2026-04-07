using Microsoft.EntityFrameworkCore;
using Recette.Data;
using Recette.Models;

namespace Recette.Services;

public class RecipeService : IRecipeService
{
    private readonly AppDbContext _context;

    public RecipeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Recipe>> GetRecipesAsync()
    {
        return await _context.Recipes
            .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
            .OrderBy(r => r.Name)
            .ToListAsync();
    }

    public async Task<Recipe?> GetRecipeByIdAsync(int id)
    {
        return await _context.Recipes
            .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddRecipeAsync(Recipe recipe, List<RecipeIngredient> ingredients)
    {
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        // Associer les ingrédients à la recette
        foreach (var ri in ingredients)
        {
            ri.RecipeId = recipe.Id;
            _context.RecipeIngredients.Add(ri);
        }
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRecipeAsync(Recipe recipe, List<RecipeIngredient> ingredients)
    {
        // Supprimer les anciens liens ingrédients
        var existing = _context.RecipeIngredients.Where(ri => ri.RecipeId == recipe.Id);
        _context.RecipeIngredients.RemoveRange(existing);

        _context.Recipes.Update(recipe);
        await _context.SaveChangesAsync();

        // Ajouter les nouveaux
        foreach (var ri in ingredients)
        {
            ri.RecipeId = recipe.Id;
            _context.RecipeIngredients.Add(ri);
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRecipeAsync(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe != null)
        {
            // Les RecipeIngredients seront supprimés en cascade
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Recipe>> SearchRecipesAsync(string? name, string? category, string? cuisineType, double? maxCalories)
    {
        // LINQ : filtrage dynamique
        var query = _context.Recipes
            .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(r => r.Name.ToLower().Contains(name.ToLower()));

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(r => r.Category == category);

        if (!string.IsNullOrWhiteSpace(cuisineType))
            query = query.Where(r => r.CuisineType == cuisineType);

        var recipes = await query.OrderBy(r => r.Name).ToListAsync();

        // Filtrage calorique en mémoire (car TotalCalories est calculé)
        if (maxCalories.HasValue)
            recipes = recipes.Where(r => r.TotalCalories <= maxCalories.Value).ToList();

        return recipes;
    }

    public async Task<DashboardStats> GetDashboardStatsAsync()
    {
        var recipes = await GetRecipesAsync();
        var totalIngredients = await _context.Ingredients.CountAsync();

        var stats = new DashboardStats
        {
            TotalRecipes = recipes.Count,
            TotalIngredients = totalIngredients,
            AverageCalories = recipes.Count > 0
                ? Math.Round(recipes.Average(r => r.TotalCalories), 1)
                : 0,
            MostCaloricRecipe = recipes.OrderByDescending(r => r.TotalCalories).FirstOrDefault(),

            // LINQ : groupement par catégorie
            RecipesByCategory = recipes
                .GroupBy(r => r.Category)
                .ToDictionary(g => g.Key, g => g.Count()),

            // LINQ : groupement par type de cuisine
            RecipesByCuisine = recipes
                .GroupBy(r => r.CuisineType)
                .ToDictionary(g => g.Key, g => g.Count())
        };

        return stats;
    }
}