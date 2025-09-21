using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Ratings
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CarID { get; set; }
        [Required]
        public int TotalStars { get; set; }
        [Required]
        public int TotalRaters { get; set; }

        [ForeignKey("CarID")]
        public Car? Car { get; set; }
    }
}
