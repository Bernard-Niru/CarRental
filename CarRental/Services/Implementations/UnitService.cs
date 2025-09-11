using CarRental.DTOs;
using CarRental.Models;
using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Services.Implementations
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepo;
        private readonly IImageRepository _imageRepo;
        private readonly ICarRepository _carRepo;

        public UnitService(IUnitRepository unitRepo, IImageRepository imageRepo, ICarRepository carRepo)
        {
            _unitRepo = unitRepo;
            _imageRepo = imageRepo;
            _carRepo = carRepo;
        }

        public IEnumerable<UnitDTO> GetAll()
        {
            var units = _unitRepo.GetAll();
            var images = _imageRepo.GetAll();

            var dto = from u in units
                      join i in images on u.CarID equals i.CarID into ui
                      from img in ui.DefaultIfEmpty()
                      select new UnitDTO
                      {
                          UnitID = u.UnitID,
                          CarID = u.CarID,
                          PlateNumber = u.PlateNumber,
                         
                      };

            return dto.ToList();
        }

        public async Task AddUnitsAsync(AddUnitsViewModel model)
        {
            if (model == null || model.Units == null || model.Units.Count == 0)
                throw new ArgumentException("No units provided.");


            foreach (var unitGroup in model.Units)
            {
                //if (!_carRepo.Exists(unitGroup.CarID))
                //    throw new Exception($"CarID {unitGroup.CarID} does not exist.");

                // Add each plate number as a unit for this car
                foreach (var plate in unitGroup.PlateNumbers)
                {
                    if (string.IsNullOrWhiteSpace(plate))
                        continue;

                    var unit = new Unit
                    {
                        CarID = unitGroup.CarID,
                        PlateNumber = plate
                    };

                    _unitRepo.Add(unit);
                }

              
                
            }
        }
    }
}
