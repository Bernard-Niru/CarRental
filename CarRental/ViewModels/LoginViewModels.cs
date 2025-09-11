using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class LoginViewModels
    {
        [Required]
        public string Username { get; set; }
        [Required] 
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
