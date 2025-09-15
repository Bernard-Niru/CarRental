using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class ImageViewModel
    {
        public int CarID { get; set; }

        public byte[] ImageData { get; set; }

        public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();       
    }
}

