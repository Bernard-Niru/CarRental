using CarRental.Enums.CarEnums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Car
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

        [Required(ErrorMessage = "Please enter the number of seats.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of seats must be a positive number.")]
        public int No_of_Seats { get; set; }

        [Required]
        public double Ratings { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Rental rate must be positive")]
        public decimal RentalRate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        // Navigation properties

        [ForeignKey("BrandID")]
        public Brand? Brand { get; set; }
        public int UnitCount { get; set; }
        public int AvailableUnit {  get; set; }

        public ICollection<Image>? Images { get; set; }

        public ICollection<Unit>? Units { get; set; }

        public ICollection<Request>? Requests { get; set; }

        public ICollection<Notification>? notifications { get; set; }
    }
}
