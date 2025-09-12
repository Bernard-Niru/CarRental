using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces
{
    public interface IImageService
    {
        //void Add(Image image);
        //IEnumerable<Image> GetAll(); 
        //Image GetById(int id);
        //void Delete(int id);
        IEnumerable<ImageViewModel> GetImgsByCarID(int carID);
        void Add(List<Image> images);
    }
}
