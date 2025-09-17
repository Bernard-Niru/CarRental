

using CarRental.Models;
using CarRental.DTOs;
using CarRental.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface IUnitService
    {

        List<string> Add(List<Unit> units);
        //Task Add(AddUnitsViewModel model);
        IEnumerable<UnitDTO> GetByCarID(int carid);
        IEnumerable<UnitDTO> GetAll();
        void ChangeAvailability(int id);
        List<UnitDTO> GetUnit(int id);


    }
}