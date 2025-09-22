using CarRental.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRental.ViewModels
{
    public class CustomerViewModel
    {
        public Dictionary<string, List<CarDTO>> CarsByRatingClass { get; set; }

        public IEnumerable<CarDTO> Cars { get; set; } = new List<CarDTO>();

        public IEnumerable<SelectListItem> BrandOptions { get; set; }

        public IEnumerable<CarDTO> TopCars { get; set; }

        public IEnumerable<NotificationDTO> Notifications { get; set; } = new List<NotificationDTO>();



    }
}
