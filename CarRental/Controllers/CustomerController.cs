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

namespace CarRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICarService _carService;
        private readonly IRequestService _requestService;
        private readonly IBrandService _brandService;
        private readonly IUserService _userService;

        public CustomerController(ICarService carService , IRequestService requestService, IBrandService brandService, IUserService userService)
        {
            _carService = carService;
            _requestService = requestService;
            _brandService = brandService;
            _userService = userService;
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




        public IActionResult Ratings(int rating)
        {
            int CarID = 2;
           _carService.AddRating(rating, CarID);
            return View("Ratings");
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

        [HttpPost]
        public IActionResult Update(ProfileViewModel vm)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("Profile", vm);
            //}

            var result = _userService.UpdateUser(vm);

            if (result == "Profile updated successfully!")
                TempData["Success"] = result;
            else
                TempData["Error"] = result;

            return RedirectToAction("ProfileEdit");
        }














































    }

}
