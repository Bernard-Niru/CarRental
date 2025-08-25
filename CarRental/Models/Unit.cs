using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Unit
    {
        [Key]
        public int UnitID { get; set; }

        [Required]
        public int CarID { get; set; }

        [Required]
        [StringLength(10)]
        public string PlateNumber { get; set; }

        [Required]
        public bool IsAvailble { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        // Navigation properties

        [ForeignKey("CarID")]
        public Car? Car { get; set; }
        
        public ICollection<Booking>? Bookings { get; set; }
    }
}
