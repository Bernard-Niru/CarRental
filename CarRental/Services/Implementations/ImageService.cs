using CarRental.DTOs;
using CarRental.Mappings;
using CarRental.Models;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;

namespace CarRental.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _repo;
    
        public ImageService(IImageRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<ImageDTO> GetByCarID(int carid)
        {
            var images = _repo.GetByCarId(carid);
            var imageDTOs = images.Select(u => new ImageDTO
            {
               Id = u.ImageID,
               CarId = u.CarID,
                ImageBase64 = Convert.ToBase64String(u.ImageData)
            });
            return imageDTOs;
        }
        public void Add(List<Image> images)
        {
            foreach (var image in images)
            {
                _repo.Add(image);
            }
        }
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
