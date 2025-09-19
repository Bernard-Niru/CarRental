using CarRental.Enums.UserEnums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Notification
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int CarID { get; set; }

        [Required]
        public Purpose purpose { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public bool IsViewed { get; set; }

        // Navigation properties

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [ForeignKey("CarID")]
        public Car? Car { get; set; }
    }
}
