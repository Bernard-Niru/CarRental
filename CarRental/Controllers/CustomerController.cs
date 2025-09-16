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

        public CustomerController(ICarService carService , IRequestService requestService ,IBrandService brandService)
        {
            _carService = carService;
            _requestService = requestService;
            _brandService = brandService;
        }

        public IActionResult SignOut()
        {
            return RedirectToAction("Index","Home");
        }
        public IActionResult HomePage()
        {
            ViewData["HideNavbar"] = true;
            return View("CustomerHomepage/Homepage");
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
            return View();
        }

        public IActionResult ViewPage()
        {
            var Car = _carService.GetAll();
            return View(Car);
        }

        [HttpPost]
        public IActionResult AddRequest(RequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var cars = _carService.GetAll(); // You need to repopulate the model
                ViewData["FormErrors"] = "Please fix the form errors.";
                return View("ViewPage", cars); // Pass the cars list again
            }
            int id = int.Parse(Role.Id);
            model.UserID = id;
            _requestService.Add(model);
            return RedirectToAction("ViewPage");
        }
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


    }
    
}
