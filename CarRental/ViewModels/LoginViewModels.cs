using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class LoginViewModels
    {

        public string Usernamelogin { get; set; }
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password must be exactly 8 characters long.")]
        [Required]
        public string Passwordlogin { get; set; }
    }
}
