using Recette.Models;

namespace Recette.Services;

public interface IRecipeService
{
    Task<List<Recipe>> GetRecipesAsync();
    Task<Recipe?> GetRecipeByIdAsync(int id);
    Task AddRecipeAsync(Recipe recipe, List<RecipeIngredient> ingredients);
    Task UpdateRecipeAsync(Recipe recipe, List<RecipeIngredient> ingredients);
    Task DeleteRecipeAsync(int id);

    // Filtrage et recherche (LINQ)
    Task<List<Recipe>> SearchRecipesAsync(string? name, string? category, string? cuisineType, double? maxCalories);

    // Statistiques pour le dashboard
    Task<DashboardStats> GetDashboardStatsAsync();
}

public class DashboardStats
{
    public int TotalRecipes { get; set; }
    public int TotalIngredients { get; set; }
    public double AverageCalories { get; set; }
    public Recipe? MostCaloricRecipe { get; set; }
    public Dictionary<string, int> RecipesByCategory { get; set; } = new();
    public Dictionary<string, int> RecipesByCuisine { get; set; } = new();
}