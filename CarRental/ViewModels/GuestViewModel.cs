using CarRental.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CarRental.ViewModels
{
    public class GuestPageViewModel
    {
        public Dictionary<string, List<CarDTO>> CarsByRatingClass { get; set; }

        public IEnumerable<CarDTO?> Cars { get; set; }
        public IEnumerable<SelectListItem> BrandOptions { get; set; }

        public IEnumerable<CarDTO> TopCars { get; set; }
    }
}