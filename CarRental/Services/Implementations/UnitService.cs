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
        public void Add(List<Unit> units)
        {
            foreach (var unit in units)
            {
                _unitRepo.Add(unit);
                
            }
            
        }
        public void ChangeAvailability (int id) 
        {
            _unitRepo.ChangeAvailability(id);
        }


    }
}
