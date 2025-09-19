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
        public IEnumerable<UnitDTO> GetByCarID(int carid)
        {
            var units = _unitRepo.GetByCarID(carid);
            var unitDTOs = units.Select(u => new UnitDTO {
                UnitID = u.UnitID,
                CarID = u.CarID,
                PlateNumber = u.PlateNumber,
                IsAvailble = u.IsAvailble
                });
            return unitDTOs;
        }
        public int Add(List<Unit> units)
        {
            int count = 0;
            var duplicates = new List<string>();

            foreach (var unit in units)
            {
                bool isPresent = _unitRepo.CheckUnit(unit.PlateNumber);

                if (!isPresent)
                {
                    _unitRepo.Add(unit);
                    count++;
                }
                else
                {
                    duplicates.Add(unit.PlateNumber);
                    
                }
            }

            return (count);
        }

        public string ChangeAvailability (int id) 
        {
           return _unitRepo.ChangeAvailability(id);
        }
        public List<UnitDTO> GetUnit(int id)
        {
            var Units = _unitRepo.GetUnitsByCarId(id);
            var units = Units.Select(unit => new UnitDTO
            {
                UnitID = unit.UnitID,              // ✅ Include UnitID
                PlateNumber = unit.PlateNumber,
                CarID = unit.CarID,
            }).ToList();
            return units;
        }

    }
}
