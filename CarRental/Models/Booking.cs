using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarRental.Enums.CarEnums;

namespace CarRental.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [Required]
        public int RequestID { get; set; }

        [Required]
        public int UnitID { get; set; }

        [Required]
        public bool IsPicked { get; set; }

        [Required]
        public bool IsReturned { get; set; }

        [Required]
        public DateOnly ReturnDate { get; set; }

        [Required]
        public TimeOnly ReturnTime { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Rental amount must be positive")]
        public decimal RentalAmount { get; set; }

        [Required]
        public Condition Condition { get; set; }

        [Required]
        public double Ratings { get; set; }

        [Required]
        public bool IsDeleted { get; set; }


        // Navigation property

        [ForeignKey("RequestID")]
        public Request? Request { get; set; }

        [ForeignKey("UnitID")]
        public Unit ? Unit { get; set; }
    }
}
