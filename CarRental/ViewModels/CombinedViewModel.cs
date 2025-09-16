using CarRental.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CarRental.ViewModels
{
    public class CombinedViewModel
    {
        public GuestPageViewModel GuestPage { get; set; }
        public UserViewModel User { get; set; }
        public LoginViewModels Login { get; set; }

        // Shortcuts so Razor doesn’t break
        public IEnumerable<CarDTO> Cars => GuestPage?.Cars;
        public IEnumerable<CarDTO> TopCars => GuestPage?.TopCars;
        public IEnumerable<SelectListItem> BrandOptions => GuestPage?.BrandOptions;
    }
}
