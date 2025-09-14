using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Mappings
{
    public class CarMapper
    {
        public static Car ToModel(CarViewModel model)
        {
            return new Car
            {
                CarID = model.CarID,
                CarName = model.CarName,
                BrandID = model.BrandID,
                CarType = model.CarType,
                FuelType = model.FuelType,
                Color = model.Color,
                No_of_Seats = model.No_of_Seats,
                Ratings = model.Ratings,
                RentalRate = model.RentalRate,
                IsDeleted = false,

            };
        }
        public static CarViewModel ToViewModel(Car car)
        {
            if (car == null) return null;

            return new CarViewModel
            {
                CarID = car.CarID,
                CarName = car.CarName,
                BrandID = car.BrandID,
                CarType = car.CarType,
                FuelType = car.FuelType,
                Color = car.Color,
                No_of_Seats = car.No_of_Seats,
                Ratings = car.Ratings,
                RentalRate = car.RentalRate,

            };
        }

        
    }
}
