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

    public async Task AddRecipeAsync(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRecipeAsync(Recipe recipe)
    {
        var existingRecipe = await _context.Recipes
            .Include(r => r.RecipeIngredients)
            .FirstOrDefaultAsync(r => r.Id == recipe.Id);

        if (existingRecipe == null)
        {
            throw new KeyNotFoundException("La recette à modifier n'existe pas.");
        }

        // Mise à jour des propriétés principales
        _context.Entry(existingRecipe).CurrentValues.SetValues(recipe);

        // Synchronisation propre des ingrédients de la recette
        // 1. Supprimer les ingrédients qui ne sont plus dans la liste modifiée
        foreach (var existingIngredient in existingRecipe.RecipeIngredients.ToList())
        {
            if (!recipe.RecipeIngredients.Any(ri => ri.IngredientId == existingIngredient.IngredientId))
            {
                _context.Remove(existingIngredient);
            }
        }

        // 2. Ajouter ou mettre à jour les ingrédients restants/nouveaux
        foreach (var ri in recipe.RecipeIngredients)
        {
            var existingIngredient = existingRecipe.RecipeIngredients
                .FirstOrDefault(e => e.IngredientId == ri.IngredientId);

            if (existingIngredient == null)
            {
                existingRecipe.RecipeIngredients.Add(new RecipeIngredient
                {
                    IngredientId = ri.IngredientId,
                    Quantity = ri.Quantity
                });
            }
            else
            {
                existingIngredient.Quantity = ri.Quantity;
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteRecipeAsync(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe != null)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }
    }
}