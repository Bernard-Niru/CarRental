using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Mappings
{
    public class BrandMapper
    {
        public static Brand ToModel(BrandViewModel model) 
        {
            return new Brand
            {
                BrandID = model.BrandID,
                BrandName = model.BrandName,
                IsDeleted = false
            };
        }
    }
}
