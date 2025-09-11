using CarRental.ViewModels;
using CarRental.Models;
using CarRental.DTOs;
namespace CarRental.Mappings
{
    public static class ImageMapping
    {
        public static ImageDTO ToDTO(Image image)
        {
            if (image == null || image.ImageData == null) return null;

            string mime = string.IsNullOrEmpty(image.MimeType) ? "image/jpeg" : image.MimeType;

            return new ImageDTO
            {
                Id = image.ImageID,
                CarId = image.CarID,
                
                ImageBase64 = $"data:{mime};base64,{Convert.ToBase64String(image.ImageData)}",
                ImageMimeType = mime
            };
        }
    }

}
