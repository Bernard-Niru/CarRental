using CarRental.Models;

namespace CarRental.Repositories.Interfaces
{
    public interface IImageRepository
    {
        void Add(Image image);
        IEnumerable<Image> GetAll();
        Image GetById(int id);
        void Delete(int id);
    }
}
