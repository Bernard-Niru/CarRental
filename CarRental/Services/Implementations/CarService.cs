using CarRental.DTOs;
using CarRental.Mappings;
using CarRental.Models;
using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                }
            });

               

            return carDTOs;
        }
        public GuestPageViewModel GetAvailableCar()
        {
            List<int> Carid = new List<int>();
            

            var cars = _repo.GetAvailableCars();

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
            //==================GetTopRatedCars==========
            var topCars = carDTOs
               .OrderByDescending(c => c.Ratings)
               .Take(5)
               .ToList();

            var dailyCars = GetDailyCars(carDTOs, 6);

            //============GetBrand=============
            var brands = _brandService.GetAll()
                          .Select(b => new SelectListItem
                          {
                              Value = b.BrandID.ToString(),
                              Text = b.BrandName
                          })
                          .ToList();

            return new GuestPageViewModel
            {
                Cars = dailyCars,
                BrandOptions = brands,
                TopCars = topCars
            };

        }
        public void ChangeUnitCount(int Carid,int Count)
        {
            var UnitCount = _repo.GetCounts(Carid);
            int finalcount = UnitCount.UnitCount + Count;
            _repo.AddUnitCount(Carid, finalcount);
        }

        public void ChangeAvailableCount(int Carid, int Count)
        {
            var Counts = _repo.GetCounts(Carid);
            if (Count > 0 ||  Counts.AvailableUnit>0)
            {
                int finalcount = Counts.AvailableUnit + Count;
                _repo.AddAvailableUnitCount(Carid, finalcount);
            }
        }


        public string Update(CarDTO model)
        {
            var Car = CarMapper.DTOToModel(model);
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

        //public GuestPageViewModel GetTopRatedCars()
        //{ 
        //    var allCars = GetAll(); // existing method that gets all cars

        //    var topCars = allCars
        //        .OrderByDescending(c => c.Ratings)
        //        .Take(5)
        //        .ToList();

        //    return new GuestPageViewModel
        //    {
        //        Cars = topCars
        //    };
        //}
        private List<CarDTO> GetDailyCars(IEnumerable<CarDTO> allCars, int maxCount)
        {
            if (allCars == null || !allCars.Any())
                return new List<CarDTO>();

            // Seed based on today's date ? same 6 cars per day
            int seed = DateTime.Today.Year * 10000 + DateTime.Today.Month * 100 + DateTime.Today.Day;
            var rng = new Random(seed);

            // Shuffle and take top 'maxCount'
            return allCars.OrderBy(c => rng.Next()).Take(maxCount).ToList();
        }


        //public List<UnitDTO> GetUnit(int id)
        //{
        //    var Units = _repo.GetUnitsByCarId(id);
        //    var units = Units.Select(unit => new UnitDTO
        //    {
        //        UnitID = unit.UnitID,              // ✅ Include UnitID
        //        PlateNumber = unit.PlateNumber,
        //        CarID = unit.CarID,
        //    }).ToList();
        //    return units;
        //}


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
        


