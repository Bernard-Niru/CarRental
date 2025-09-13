using CarRental.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CarRental.ViewModels
{
    public class GuestPageViewModel
    {
        public IEnumerable<CarDTO> Cars { get; set; }
        public IEnumerable<SelectListItem> BrandOptions { get; set; }
    }
}