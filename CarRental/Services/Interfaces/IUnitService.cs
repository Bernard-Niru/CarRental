

using CarRental.Models;
using CarRental.DTOs;
using CarRental.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface IUnitService
    {

       int Add(List<Unit> units);
        //Task Add(AddUnitsViewModel model);
        IEnumerable<UnitDTO> GetByCarID(int carid);
        IEnumerable<UnitDTO> GetAll();
        string ChangeAvailability(int id);
        List<UnitDTO> GetUnit(int id);
        void Delete(int id);
        void ChangeAvailability(string plateNumber);


    }
}