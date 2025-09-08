using CarRental.DTOs;
using CarRental.Mappings;
using CarRental.Models;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;

namespace CarRental.Services.Implementations
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepo;
        private readonly IImageRepository _imageRepo;

        public UnitService(IUnitRepository unitRepo, IImageRepository imageRepo)
        {
            _unitRepo = unitRepo;
            _imageRepo = imageRepo;
        }

        public void Add(Unit unit)
        {
            _unitRepo.Add(unit);
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
                          ImageBase64 = img != null
                              ? $"data:image/png;base64,{Convert.ToBase64String(img.ImageData)}"
                              : "/images/default.png"
                      };

            return dto.ToList();
        }

        public async Task AddWithImagesAsync(AddUnitsViewModel model)
        {
            foreach (var unitViewModel in model.Units)
            {
                
                var unit = UnitMapper.ToModel(unitViewModel);
                _unitRepo.Add(unit);

                
                if (unitViewModel.ImageFile != null && unitViewModel.ImageFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await unitViewModel.ImageFile.CopyToAsync(ms);
                    var image = UnitMapper.ToImage(unitViewModel, ms.ToArray());
                    _imageRepo.Add(image);
                }
            }
        }
    }
}
