using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class ImageViewModel
    {
        [Key]
        public int ImageID { get; set; }

        [Required]
        public int CarID { get; set; }

        [Required]
        public byte[] ImageData { get; set; }

        public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();
    }
}
