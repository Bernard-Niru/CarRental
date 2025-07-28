using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }

        [Required]
        public string BrandName { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        // Navigation property

        public ICollection<Car>? Cars { get; set; }
    }
}
