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
    }
}
