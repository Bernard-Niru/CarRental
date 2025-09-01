using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;

namespace CarRental.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _repo;

        public ImageService(IImageRepository repo)
       {
            _repo = repo;
       }
    }
}
