using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class ChangePasswordViewModel
    {
        public int UserId { get; set; }

        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password must be exactly 8 characters long.")]
        [Required]
        public string OldPassword { get; set; }

        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password must be exactly 8 characters long.")]
        [Required]
        public string NewPassword { get; set; }

        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password must be exactly 8 characters long.")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
