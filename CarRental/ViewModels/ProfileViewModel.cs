using CarRental.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRental.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }


        public GuestPageViewModel GuestPage { get; set; }

        public IEnumerable<CarDTO> Cars => GuestPage?.Cars;
        public IEnumerable<CarDTO> TopCars => GuestPage?.TopCars;
        public IEnumerable<SelectListItem> BrandOptions => GuestPage?.BrandOptions;



    }
}