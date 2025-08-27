using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Request
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int CarID { get; set; }

        [Required]
        public DateOnly PickupDate { get; set; }

        [Required]
        public TimeOnly PickupTime { get; set; }

        [Required]
        public DateOnly ReturnDate { get; set; }

        [Required]
        public TimeOnly ReturnTime { get; set; }

        [Required]
        public bool IsAccepted { get; set; }

        [Required]
        public bool IsRejected { get; set; }

        // Navigation property

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [ForeignKey("CarID")]
        public Car? Car { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
