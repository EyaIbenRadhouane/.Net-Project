using System.ComponentModel.DataAnnotations;
using Recette.Attributes;

namespace Recette.Models;

// 🎨 Catégories d'ingrédients pour la visualisation
public enum IngredientCategory
{
    Legumes,       // Légumes
    Fruits,        // Fruits
    Proteines,     // Protéines (viande, poisson, œufs)
    Cereales,      // Céréales (riz, blé, pâtes)
    Produits_Laitiers,  // Produits laitiers
    Boissons,      // Boissons
    Huiles,        // Huiles et graisses
    Condiments,    // Condiments et épices
    Legumineuses,  // Légumineuses (lentilles, pois chiches)
    Autre          // Autre
}

[UniqueNameUnit] //
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

    // 🥗 Valeurs nutritionnelles (pour 100g ou par unité)
    [Range(0, 100, ErrorMessage = "Les protéines doivent être entre 0 et 100g.")]
    public double? Proteins { get; set; } // g (grammes)

    [Range(0, 100, ErrorMessage = "Les glucides doivent être entre 0 et 100g.")]
    public double? Carbohydrates { get; set; } // g (glucides)

    [Range(0, 100, ErrorMessage = "Les lipides doivent être entre 0 et 100g.")]
    public double? Lipids { get; set; } // g (graisses)

    [Range(0, 100, ErrorMessage = "Les fibres doivent être entre 0 et 100g.")]
    public double? Fiber { get; set; } // g (fibres)

    [Range(0, 100, ErrorMessage = "Le sucre doit être entre 0 et 100g.")]
    public double? Sugar { get; set; } // g (sucres)

    // 🎨 Catégorie d'ingrédient
    [Required(ErrorMessage = "La catégorie est obligatoire.")]
    public IngredientCategory Category { get; set; } = IngredientCategory.Autre;

    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    public string? ImageUrl { get; set; }
    public string? ChefId { get; set; }
}
