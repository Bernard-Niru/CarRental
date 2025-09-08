using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Mappings
{
    public static class UnitMapper
    {
        public static Unit ToModel(UnitImageViewModel viewModel)
        {
            return new Unit
            {
                CarID = viewModel.CarID,
                PlateNumber = viewModel.PlateNumber,
                IsAvailble = true,
                IsDeleted = false
            };
        }

        public static Image ToImage(UnitImageViewModel viewModel, byte[] imageData)
        {
            return new Image
            {
                CarID = viewModel.CarID,
                ImageData = imageData,
                IsDeleted = false
            };
        }
    }
}
