using CarRental.Models;

namespace CarRental.Repositories.Interfaces
{
    public interface IImageRepository
    {
        //Task AddImagesAsync(int carId, List<byte[]> imagesData);


        void Add(Image image);
        Task SaveChangesAsync();
        IEnumerable<Image> GetByCarId(int carId);
        void Delete(int id);
        IEnumerable<Image> GetAll();
    }
}
