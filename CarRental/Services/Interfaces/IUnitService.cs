

using CarRental.Models;
using CarRental.DTOs;
using CarRental.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface IUnitService
    {

        Task AddUnitsAsync(AddUnitsViewModel model);
        //Task Add(AddUnitsViewModel model);

        IEnumerable<UnitDTO> GetAll();
        
       
    }
}