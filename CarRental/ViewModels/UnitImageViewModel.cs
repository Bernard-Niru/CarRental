using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CarRental.ViewModels
{
    public class UnitImageViewModel
    {
        [Required]
        public int CarID { get; set; }

        [Required]
        [StringLength(10)]
        public string PlateNumber { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }  // Image uploaded from form
    }
}
