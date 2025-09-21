using CarRental.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public byte[]? ProfileImage { get; set; }



        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password must be exactly 8 characters long.")]
        [Required]
        public string OldPassword { get; set; }

        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password must be exactly 8 characters long.")]
        [Required]
        public string NewPassword { get; set; }

        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password must be exactly 8 characters long.")]
        [Required]
        public string ConfirmPassword { get; set; }


        public GuestPageViewModel GuestPage { get; set; }

        public IEnumerable<CarDTO> Cars => GuestPage?.Cars;
        public IEnumerable<CarDTO> TopCars => GuestPage?.TopCars;
        public IEnumerable<SelectListItem> BrandOptions => GuestPage?.BrandOptions;

        public IEnumerable<BookingDTO> bookings;



    }
}