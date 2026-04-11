using Recette.Models;

namespace Recette.Services;

public interface IRecipeService
{
    Task<List<Recipe>> GetRecipesAsync();
    Task<Recipe?> GetRecipeByIdAsync(int id);
    Task AddRecipeAsync(Recipe recipe);
    Task UpdateRecipeAsync(Recipe recipe);
    Task DeleteRecipeAsync(int id);
}