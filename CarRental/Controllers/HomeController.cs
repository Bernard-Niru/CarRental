using System.Data;
using System.Diagnostics;
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
        public IActionResult login()
        {
            
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel User)
        {
            User.Role = Enums.UserEnums.UserRole.Customer;
            if (ModelState.IsValid)
            {
                if (await _userservice.CheckAsync(User.UserName.Trim()))
                {
                    TempData["RegisterErrorMessage"] = "Username already exists";
                    return View("login");
                }

                await _userservice.AddAsync(User);
                return RedirectToAction("ViewUser", "Admin");
            }
            TempData["RegisterErrorMessage"] = "Invalid Input";
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> login(UserViewModel login)
        {
            string Username = login.UserName.Trim();
            bool userExists = await _userservice.CheckAsync(Username);

            if (userExists)
            {
                string role = _userservice.CheckPassword(login);

                if (role.Contains(','))
                {
                    string[] User = role.Split(',');
                    Role.RoleName = User[0].Trim();
                    Role.Id = User[1].Trim();
                    if (User[0] == "Admin")
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    if (User[0] == "Customer")
                    {
                        return RedirectToAction("HomePage", "Customer");
                    }
                    if (User[0] == "Staff")
                    {
                        return RedirectToAction("ViewCars", "Admin");
                    }
                }
                TempData["LoginErrorMessage"] = role;
                return View("Index", login);

            }
            TempData["LoginErrorMessage"] = "Incorrect UserName";
            return View("Index", login);
        }

        public IActionResult Index()
        {
            
            // Get all brands to populate the filter dropdown
            var brands = _brandService.GetAll()
                                      .Select(b => new SelectListItem
                                      {
                                          Value = b.BrandID.ToString(),
                                          Text = b.BrandName
                                      })
                                      .ToList();

            // Get all available cars
            var cars = _carService.GetAll();

            // Create a ViewModel to hold both lists and pass it to the view
            var guestViewModel = new GuestPageViewModel
            {
                Cars = cars,
                BrandOptions = brands
            };

            return View(guestViewModel);
        }
    

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //public IActionResult GuestPage()
        //{
        //    var Car = _carService.GetAll();
        //    return View(Car);
        //}

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
