using CarRental.DTOs;
using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces
{
    public interface IUnitService
    {
        void Add(Unit unit);
        IEnumerable<UnitDTO> GetAll();

        Task AddWithImageAsync(UnitImageViewModel model); 
    }
}
