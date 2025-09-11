using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Mappings
{
    public class ImageMapper
    {
        public static List<ImageViewModel> ToViewModelList(IEnumerable<Image> images)
        {
            return images.Select(image => new ImageViewModel
            {
                ImageID = image.ImageID,
                ImageData = image.ImageData,
            }).ToList();
        }

    }
}
