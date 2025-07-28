using System.ComponentModel.DataAnnotations;
using CarRental.Enums;

namespace CarRental.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }  

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password must be exactly 8 characters long.")]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }

        [Required]
        public bool isDeleted { get; set; }

    }
}
