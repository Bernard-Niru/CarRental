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
        public ICollection<Notification>? notifications { get; set; }
        public ICollection<Booking>? Bookings { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
//name data srch panni not ex