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
        public string AddCar(CarViewModel model)
        {
            var car = CarMapper.ToModel(model);
            return _repo.AddCar(car);

        }
        public IEnumerable<CarDTO> GetAll()
        {
            var cars = _repo.GetAll();
            var brands = _brandService.GetAll();
            var carlist = new List<CarDTO>();

            foreach (var c in cars)
            {
                var brandName = brands.FirstOrDefault(b => b.BrandID == c.BrandID)?.BrandName ?? "Unknown";
                //var image = _imageService.GetImgsByCarID(c.CarID);

                var images = _imageService.GetImgsByCarID(c.CarID);


                var imageDataList = images != null && images.Any()
                    ? images.Select(img => "data:image/jpeg;base64," + Convert.ToBase64String(img.ImageData)).ToList()
                    : new List<string> { "/images/default-car.png" };

                var imageIDs = images != null && images.Any()
                    ? images.Select(img => img.ImageID).ToList()
                    : new List<int>();


                var model = new CarDTO
                {
                    CarID = c.CarID,
                    CarName = c.CarName,
                    BrandID = c.BrandID,
                    BrandName = brandName,
                    ImageDataList = imageDataList,
                    ImageIDs = imageIDs,
                    CarType= c.CarType,
                    FuelType = c.FuelType,
                    Color = c.Color,
                    No_of_Seats = c.No_of_Seats,
                    Ratings = c.Ratings,
                    RentalRate = c.RentalRate,
                };

                carlist.Add(model);
            }

            return carlist;
        }

        public string Update(CarViewModel model)
        {
            var Car = CarMapper.ToModel(model);
            return _repo.Update(Car);
        }

        public CarViewModel GetcarByID(int id)
        {
            var car = _repo.GetByID(id);
            //if (car == null)
            //{
            // Could throw an exception or return null
            // return null;
            //}
            var user = CarMapper.ToViewModel(car);
            return user;
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



    }
}
        

