// Attributes/UniqueNameUnitAttribute.cs
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Recette.Data;
using Microsoft.EntityFrameworkCore;
using Recette.Models;

namespace Recette.Attributes;

public class UniqueNameUnitAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var context = validationContext.GetService<AppDbContext>();
        if (context == null) return ValidationResult.Success;

        var ingredient = (Ingredient)validationContext.ObjectInstance;

        bool exists = context.Ingredients.Any(i =>
            i.Name == ingredient.Name &&
            i.Unit == ingredient.Unit &&
            i.Id != ingredient.Id  // Exclure l'ingrédient lui-même lors de la modification
        );

        if (exists)
            return new ValidationResult("Un ingrédient avec ce nom et cette unité existe déjà.");

        return ValidationResult.Success;
    }
}