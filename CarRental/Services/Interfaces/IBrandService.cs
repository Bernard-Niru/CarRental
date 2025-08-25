using CarRental.DTOs;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces
{
    public interface IBrandService
    {
        IEnumerable<BrandDTO> GetAll();
        void Add(BrandViewModel model);
    }
}
