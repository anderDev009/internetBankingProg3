

using InternetBanking.Core.Application.ViewModels.BankAccount;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Users
{
    public class SaveUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar la cédula")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [StringLength(11, ErrorMessage = "Debe tener  11 numeros", MinimumLength = 11)]
        public string CardIdentificantion { get; set; }

        [Required(ErrorMessage = "Debe colocar un correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe colocar el tipo de usuario")]
        public string TypeUser { get; set; }
        public bool IsConfirm { get; set; }
        public double? Amount { get; set; }
        public bool? HasError { get; set; }
        public string? Error { get; set; }

        public string? currentPassword { get; set; }

        //Propiedades para guardar cuenta
        public decimal InitialAmmount { get; set; }
    }
}
