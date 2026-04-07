using System.ComponentModel.DataAnnotations;
namespace Recette.Models;
public class RecipeIngredient
{
    public int Id { get; set; }

    public int RecipeId { get; set; }
    public Recipe? Recipe { get; set; }

    public int IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }

    [Required(ErrorMessage = "La quantité est obligatoire.")]
    [Range(0.01, 10000, ErrorMessage = "La quantité doit être supérieure à 0.")]
    public double Quantity { get; set; }
}