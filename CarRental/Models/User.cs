using System.ComponentModel.DataAnnotations;
using CarRental.Enums.UserEnums;

namespace CarRental.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string Name { get; set; }  

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]  
        public UserRole Role { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        // Navigation properties

        public ICollection<Request>? Requests { get; set; }

    }
}
//name data srch panni not ex