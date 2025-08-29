using CarRental.Enums.UserEnums;
using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name {  get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password must be exactly 8 characters long.")]
        public string Password { get; set; }
        [Required]
        public UserRole Role { get; set; }




    }
}
