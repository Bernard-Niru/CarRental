using CarRental.Enums.CarEnums;
using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class CarViewModel
    {
        [Key]
        public int CarID { get; set; }

        [Required]
        public string CarName { get; set; }

        [Required]
        public int BrandID { get; set; }

        [Required]
        public CarType CarType { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

        [Required]
        public GearType GearType { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public int No_of_Seats { get; set; }

        [Required]
        public double Ratings { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Rental rate must be positive")]
        public decimal RentalRate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }


    }
}
