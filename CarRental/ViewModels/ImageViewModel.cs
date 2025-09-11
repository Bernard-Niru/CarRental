using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class AddImageViewModel
    {
        [Required]
        public int CarID { get; set; }

        [Required]
        public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();
    }
}

