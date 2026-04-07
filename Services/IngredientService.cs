using Microsoft.EntityFrameworkCore;
using Recette.Data;
using Recette.Models;

namespace Recette.Services;

public class IngredientService : IIngredientService
{
    private readonly AppDbContext _context;

    public IngredientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ingredient>> GetIngredientsAsync()
    {
        return await _context.Ingredients
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    public async Task<Ingredient?> GetIngredientByIdAsync(int id)
    {
        return await _context.Ingredients.FindAsync(id);
    }

    public async Task AddIngredientAsync(Ingredient ingredient)
    {
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateIngredientAsync(Ingredient ingredient)
    {
        _context.Ingredients.Update(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteIngredientAsync(int id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient != null)
        {
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }
    }
}