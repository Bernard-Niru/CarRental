using CarRental.DTOs;
using CarRental.Mappings;
using CarRental.Models;
using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Humanizer;

namespace CarRental.Services.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repo;
        private readonly IBrandService _brandService;
        private readonly IImageService _imageService;

        public CarService(ICarRepository repo, IBrandService brandService, IImageService imageService) 
        {
            _repo = repo; 
            _brandService = brandService;
            _imageService = imageService;
        }
        public int AddCar(CarViewModel model)
        {
            var car = CarMapper.ToModel(model);
            return _repo.AddCar(car);

        }
        public IEnumerable<CarDTO> GetAll()
        {
            var cars = _repo.GetAll(); // Already filtered at repo

            var carDTOs = cars.Select(car => new CarDTO
            {
                CarID = car.CarID,
                CarName = car.CarName,
                BrandID = car.BrandID,
                CarType = car.CarType,
                FuelType = car.FuelType,
                GearType = car.GearType,
                Color = car.Color,
                No_of_Seats = car.No_of_Seats,
                Ratings = car.Ratings,
                RentalRate = car.RentalRate,

                Brand = new BrandDTO
                {
                    BrandID = car.Brand.BrandID,
                    BrandName = car.Brand.BrandName
                },

                Images = car.Images?.Select(i => new ImageDTO
                {
                    Id = i.ImageID,
                    CarId = i.CarID,
                    ImageBase64 = Convert.ToBase64String(i.ImageData)
                }).ToList() ?? new(),

                Units = car.Units?.Select(u => new UnitDTO
                {
                    UnitID = u.UnitID,
                    CarID = u.CarID,
                    PlateNumber = u.PlateNumber,
                    IsAvailble = u.IsAvailble
                }).ToList() ?? new()
            });

            return carDTOs;
        }


        public string Update(CarViewModel model)
        {
            var Car = CarMapper.ToModel(model);
            return _repo.Update(Car);
        }

        public CarDTO? GetByID(int id)
        {
            var car = _repo.GetByID(id); // Assuming you updated your repo method to include related entities and filtering

            if (car == null)
            {
                return null;
            }

            var carDTO = new CarDTO
            {
                CarID = car.CarID,
                CarName = car.CarName,
                BrandID = car.BrandID,
                CarType = car.CarType,
                FuelType = car.FuelType,
                GearType = car.GearType,
                Color = car.Color,
                No_of_Seats = car.No_of_Seats,
                Ratings = car.Ratings,
                RentalRate = car.RentalRate,

                Brand = new BrandDTO
                {
                    BrandID = car.Brand.BrandID,
                    BrandName = car.Brand.BrandName
                },

                Images = car.Images?.Select(i => new ImageDTO
                {
                    Id = i.ImageID,
                    CarId = i.CarID,
                    ImageBase64 = Convert.ToBase64String(i.ImageData)
                }).ToList() ?? new(),

                Units = car.Units?.Select(u => new UnitDTO
                {
                    UnitID = u.UnitID,
                    CarID = u.CarID,
                    PlateNumber = u.PlateNumber,
                    IsAvailble = u.IsAvailble
                }).ToList() ?? new()
            };

            return carDTO;
        }

        public void Delete(int id)
        {
            var car = _repo.GetByID(id);
            if (car != null)
            {
                car.IsDeleted = true;
                _repo.Update(car); 
            }
        }

        public GuestPageViewModel GetTopRatedCars()
        {
            var allCars = GetAll(); // existing method that gets all cars

            var topCars = allCars
                .OrderByDescending(c => c.Ratings)
                .Take(5)
                .ToList();

            return new GuestPageViewModel
            {
                Cars = topCars
            };
        }
        public void AddRating(int rating,int CarId) 
        {           
            var car = _repo.GetByID(CarId);
            if (car != null)
            {
                double OldRatings = car.Ratings;
                car.Ratings = (OldRatings + Convert.ToDouble(rating)) / 2 ;
                _repo.Update(car);
            }
        }

    }
}
        


