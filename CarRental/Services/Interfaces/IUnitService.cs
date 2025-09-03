

using CarRental.Models;
using CarRental.DTOs;
using CarRental.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface IUnitService
    {
        void Add(Unit unit);
        IEnumerable<UnitDTO> GetAll();
        
        Task AddWithImagesAsync(AddUnitsViewModel model);
    }
}