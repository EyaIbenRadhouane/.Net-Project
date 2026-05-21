using System.ComponentModel.DataAnnotations;

namespace Recette.Models;

public class SignupModel
{
    [Required(ErrorMessage = "Email obligatoire.")]
    [EmailAddress(ErrorMessage = "Email invalide.")]
    public string Mail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password obligatoire.")]
    [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirmation obligatoire.")]
    public string VerifyPassword { get; set; } = string.Empty;
}