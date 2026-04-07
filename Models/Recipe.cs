using System.ComponentModel.DataAnnotations;

namespace Recette.Models;

public class Recipe
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom de la recette est obligatoire.")]
    [StringLength(150, ErrorMessage = "Le nom ne peut pas dépasser 150 caractères.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le nombre de personnes est obligatoire.")]
    [Range(1, 100, ErrorMessage = "Le nombre de personnes doit être entre 1 et 100.")]
    public int NumberOfPersons { get; set; } = 1;
    public double CaloriesRecipe{ get; set; }

    [Required(ErrorMessage = "La catégorie est obligatoire.")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le type de cuisine est obligatoire.")]
    public string CuisineType { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Les instructions ne peuvent pas dépasser 1000 caractères.")]
    public string? Instructions { get; set; }

    // une recette peut avoir plusieurs ingrédients
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();










    // Propriété calculée (non stockée en BDD)
    public double TotalCalories =>
        RecipeIngredients.Sum(ri => ri.Quantity * (ri.Ingredient?.CaloriesPerUnit ?? 0));

    public double CaloriesPerPerson =>
        NumberOfPersons > 0 ? TotalCalories / NumberOfPersons : 0;
}