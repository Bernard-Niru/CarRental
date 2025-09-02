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

        public static BrandViewModel ToViewModel(Brand brand)
        {
            return new BrandViewModel
            {
                BrandID = brand.BrandID,
                BrandName = brand.BrandName,
                
            };
        }

        public static Brand Delete(Brand brand) 
        {
            return new Brand
            {
                BrandID = brand.BrandID,
                BrandName = brand.BrandName,
                IsDeleted = true
            };
        }
    }
}
