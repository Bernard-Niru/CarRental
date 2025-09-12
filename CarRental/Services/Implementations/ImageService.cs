using CarRental.Mappings;
using CarRental.Models;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;

namespace CarRental.Services.Implementations

public class ImageService : IImageService
{
    private readonly IImageRepository -repo;

    public ImageService(IImageRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<ImageViewModel> GetImgsByCarID(int carID)
    {
        var images =  _imageRepo.GetImgsByCarID(carID);

        if (images == null || !images.Any())
        {
            // Return empty list or null, depending on your preference
            return Enumerable.Empty<ImageViewModel>();
        }

        return ImageMapper.ToViewModelList(images);
        }
        public void Add(List<Image> images)
        {
            foreach (var image in images)
            {
                _repo.Add(image);
            }
        }
    }
}