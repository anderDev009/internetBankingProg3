
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Users
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe colocar el correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        public string Password { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
