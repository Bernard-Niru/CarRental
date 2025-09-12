using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class RequestViewModel
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
    }
}
