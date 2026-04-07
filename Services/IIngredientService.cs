using Recette.Models;

namespace Recette.Services;

public interface IIngredientService
{
    Task<List<Ingredient>> GetIngredientsAsync();
    Task<Ingredient?> GetIngredientByIdAsync(int id);
    Task AddIngredientAsync(Ingredient ingredient);
    Task UpdateIngredientAsync(Ingredient ingredient);
    Task DeleteIngredientAsync(int id);
}