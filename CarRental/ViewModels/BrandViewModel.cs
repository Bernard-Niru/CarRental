using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class BrandViewModel
    {
        public int BrandID { get; set; }

        [Required]
        public string BrandName { get; set; }
    }
}
