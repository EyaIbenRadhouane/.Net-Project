using System.ComponentModel.DataAnnotations;
using Recette.Attributes;

namespace Recette.Models;

[UniqueNameUnit] // 👈 Attribut au niveau de la classe
public class Ingredient
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Les calories par unité sont obligatoires.")]
    [Range(0, 10000, ErrorMessage = "Les calories doivent être entre 0 et 10000.")]
    public double CaloriesPerUnit { get; set; }

    [Required(ErrorMessage = "L'unité de mesure est obligatoire.")]
    [StringLength(20, ErrorMessage = "L'unité ne peut pas dépasser 20 caractères.")]
    public string Unit { get; set; } = string.Empty;

    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}