using System.Data;
using System.Diagnostics;
using CarRental.DTOs;
using CarRental.Models;
using CarRental.Services.Implementations;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRental.Controllers
{//username
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBrandService _brandService;
        private readonly ICarService _carService;
        private readonly IUserService _userservice;

        public HomeController(ILogger<HomeController> logger , ICarService carService , IUserService userService, IBrandService brandService)
        {
            _logger = logger;
            _carService = carService;
            _userservice = userService;
            _brandService = brandService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            TempData["LoginErrorMessage"] = "1";
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Index()
        {
            // 1?? Get brands
            var brands = _brandService.GetAll()
                                      .Select(b => new SelectListItem
                                      {
                                          Value = b.BrandID.ToString(),
                                          Text = b.BrandName
                                      })
                                      .ToList();

            // 2?? Get all cars (DTOs)
            var allCars = _carService.GetAll(); // IEnumerable<CarDTO>

            // 3?? Pick 6 daily cars
            var dailyCars = GetDailyCars(allCars, 6);

            // 4?? Top 5 cars for Swiper
            var topCars = _carService.GetTopRatedCars().Cars;

            // 5?? Build ViewModel
            var guestViewModel = new GuestPageViewModel
            {
                Cars = dailyCars,       // only 6 cars
                BrandOptions = brands,
                TopCars = topCars
            };

            return View(guestViewModel);
        }
        // Helper: pick daily 6 cars
        private List<CarDTO> GetDailyCars(IEnumerable<CarDTO> allCars, int maxCount)
        {
            if (allCars == null || !allCars.Any())
                return new List<CarDTO>();

            // Seed based on today's date ? same 6 cars per day
            int seed = DateTime.Today.Year * 10000 + DateTime.Today.Month * 100 + DateTime.Today.Day;
            var rng = new Random(seed);

            // Shuffle and take top 'maxCount'
            return allCars.OrderBy(c => rng.Next()).Take(maxCount).ToList();
        }

        //[HttpPost]
        //public IActionResult Index(string Open = "defaultValue")
        //{
        //    if (login != null)
        //    {
        //        return RedirectToAction("ViewCars", "Admin");

        //     }
        //    return View("Index");

        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult GuestPage()
        {
            var Car = _carService.GetAll();
            return View(Car);
        }

        //public IActionResult GuestPage()
        //{
        //    // Get all brands to populate the filter dropdown
        //    var brands = _brandService.GetAll()
        //                              .Select(b => new SelectListItem
        //                              {
        //                                  Value = b.BrandID.ToString(),
        //                                  Text = b.BrandName
        //                              })
        //                              .ToList();

        //    // Get all available cars
        //    var cars = _carService.GetAll();

        //    // Create a ViewModel to hold both lists and pass it to the view
        //    var guestViewModel = new GuestPageViewModel
        //    {
        //        Cars = cars,
        //        BrandOptions = brands
        //    };

        //    return View(guestViewModel);
        //}


    }
   
}
