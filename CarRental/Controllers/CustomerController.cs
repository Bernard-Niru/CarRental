using CarRental.Models;
using CarRental.Services.Implementations;
using CarRental.DTOs;
using System.Data;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using CarRental.Enums.CarEnums;
using CarRental.Enums.UserEnums;

namespace CarRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICarService _carService;
        private readonly IRequestService _requestService;
        private readonly IBrandService _brandService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IBookingService _bookingService;


        public CustomerController(ICarService carService , IRequestService requestService ,IBrandService brandService,INotificationService notificationService, IUserService userService, IBookingService bookingService)

        {
            _carService = carService;
            _requestService = requestService;
            _brandService = brandService;
            _notificationService = notificationService;
            _userService = userService;
            _bookingService = bookingService;

        }

        public IActionResult SignOut()
        {
            return RedirectToAction("Index","Home");
        }
        public IActionResult HomePage()
        {
            var vm = _carService.GetAvailableCarsForCustomer();

            if (!ModelState.IsValid)
            {
                ViewData["FormErrors"] = "Please fix the form errors.";
            }

            return View("HomePage", vm);
        }

        public IActionResult Index()
        {
            //var brands = _brandService.GetAll()
            //              .Select(b => new SelectListItem
            //              {
            //                  Value = b.BrandID.ToString(),
            //                  Text = b.BrandName
            //              })
            //              .ToList(); 

            //var allCars = _carService.GetAll();
            //var dailyCars = GetDailyCars(allCars, 6);
            //var topCars = _carService.GetTopRatedCars().Cars;

            //var guestViewModel = new GuestPageViewModel
            //{
            //    Cars = dailyCars,
            //    BrandOptions = brands,
            //    TopCars = topCars
            //};
            return View("HomePage");
        }

        public IActionResult ViewPage()
        {
            var Car = _carService.GetAvailableCar();
            return View(Car);
        }


        [HttpPost]
        public IActionResult AddRequest(RequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reuse service that always builds a CustomerViewModel
                var vm = _carService.GetAvailableCarsForCustomer();

                ViewData["FormErrors"] = "Please fix the form errors.";
                return View("HomePage", vm); 
            }

            int userId = Session.UserID;
            var userDto = _userService.GetUserById(userId);

            if (userDto == null)
                return NotFound();

            model.UserID = userDto.Id; // get current user id from session
            _requestService.Add(model);

            return RedirectToAction("HomePage");
        }

        //[HttpPost]
        //public IActionResult AddRequest(RequestViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var cars = _carService.GetAll(); // You need to repopulate the model
        //        ViewData["FormErrors"] = "Please fix the form errors.";
        //        return View("HomePage", cars); // Pass the cars list again
        //    }
        //    model.UserID = HttpContext.Session.GetInt32("UserID") ?? 0; // get current user id from session
        //    _requestService.Add(model);
        //    return RedirectToAction("HomePage");
        //}
        private List<CarDTO> GetDailyCars(IEnumerable<CarDTO> allCars, int maxCount)
        {
            if (allCars == null || !allCars.Any())
                return new List<CarDTO>();

            int seed = DateTime.Today.Year * 10000 + DateTime.Today.Month * 100 + DateTime.Today.Day;
            var rng = new Random(seed);

            return allCars.OrderBy(c => rng.Next()).Take(maxCount).ToList();
        }




        public IActionResult Ratings(int rating,int CarID)
        {
            
           _notificationService.AddRatings(rating, CarID);
            return RedirectToAction("Notification");
        }

      
        public IActionResult ProfileEdit()
        {
            int userId = Session.UserID; // from session
            var userDto = _userService.GetUserById(userId);

            if (userDto == null)
                return NotFound();

            var vm = new ProfileViewModel
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Email = userDto.Email,
                UserName = userDto.UserName,
                Role = userDto.Role.ToString()
            };

            return View("ProfileEdit" , vm);
        }
        public IActionResult Profile() 
        {
            
              int userId = Session.UserID; // from session
              var userDto = _userService.GetUserById(userId);

              if (userDto == null)
                  return NotFound();

            var bookings = _bookingService.GetUserBookingHistory(Session.UserID);

            //if (bookings == null || !bookings.Any())
            //{
            //    return NotFound("No picked bookings found for this user.");
            //}

            var vm = new ProfileViewModel
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Email = userDto.Email,
                UserName = userDto.UserName,
                Role = userDto.Role.ToString(),
                bookings = bookings
            };
              return View("Profile",vm);
        }

        public IActionResult Notification() 
        {
            var notification = _notificationService.GetAll(Session.UserID);
            return View(notification);

            
        }
        [HttpPost]
        public IActionResult Update(ProfileViewModel vm)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("Profile", vm);
            //}
            int id = Session.UserID;

            var result = _userService.UpdatePassword(vm,id);

            if (result == "Password updated successfully!")
                TempData["Success"] = result;
            else
                TempData["Error"] = result;

            return RedirectToAction("Profile");

        }
        public IActionResult GetUserBookingHistory()
        {
            var bookings = _bookingService.GetUserBookingHistory(Session.UserID);

            if (bookings == null || !bookings.Any())
            {
                return NotFound("No picked bookings found for this user.");
            }


            return View("Profile",bookings);
        }

        //[HttpPost]
        //public async Task<IActionResult> UpdateProfileImage(IFormFile profileImage)
        //{
        //    if (profileImage == null || profileImage.Length == 0)
        //    {
        //        return BadRequest("No file selected");
        //    }
        //    int userId = Session.UserID; // Or use ClaimsPrincipal if available

        //    using var ms = new MemoryStream();
        //    await profileImage.CopyToAsync(ms);
        //    byte[] imageBytes = ms.ToArray();

        //    var result = await _userService.UpdateProfileImageAsync(userId, imageBytes);

        //    return View("Profile");
        //}
        [HttpPost]
        public async Task<IActionResult> UpdateProfileImage(IFormFile profileImage)
        {
            if (profileImage == null || profileImage.Length == 0)
            {
                TempData["Error"] = "No file selected";
                return RedirectToAction("Profile");
            }

            int userId = Session.UserID; // or from ClaimsPrincipal

            using var ms = new MemoryStream();
            await profileImage.CopyToAsync(ms);
            byte[] imageBytes = ms.ToArray();

            var result = await _userService.UpdateProfileImageAsync(userId, imageBytes);

            if (string.IsNullOrEmpty(result) || result != "success")
            {
                TempData["Error"] = "Image upload failed!";
                return RedirectToAction("Profile");
            }


            TempData["Success"] = "Profile image updated successfully!";

            // 🔑 Always reload user profile and return with model
            var user = _userService.GetUserById(userId);
            var vm = new ProfileViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                UserName = user.UserName,
                Role = user.Role.ToString()
            };

            return View("Profile", vm);
        }



        [HttpGet]
        public IActionResult GetProfileImage(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user?.ProfileImage == null || user.ProfileImage.Length == 0)
            {
                // Load default placeholder image from wwwroot/image
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/vector-sign-of-user-icon.jpg");
                var bytes = System.IO.File.ReadAllBytes(path);
                return File(bytes, "image/jpeg");
            }

            return File(user.ProfileImage, "image/jpeg");
        }











































    }

}
