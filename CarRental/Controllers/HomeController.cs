using System.Diagnostics;
using CarRental.Models;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{//username
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ICarService _carService;
        private readonly IUserService _userservice;

        public HomeController(ILogger<HomeController> logger , ICarService carService , IUserService userService)
        {
            _logger = logger;
            _carService = carService;
            _userservice = userService;

        }
        [HttpGet]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(LoginViewModels login)
        {
            string Username = login.Username.Trim();
            bool userExists = await _userservice.CheckAsync(Username);

            if (userExists)
            {
                string role = _userservice.CheckPassword(login);
                if(role == "1")
                {
                    return RedirectToAction("ViewUser", "Admin");
                }
                if(role == "2")
                {
                    return RedirectToAction("ViewBrands", "Admin");
                }
                if (role == "3")
                {
                    return RedirectToAction("ViewCars", "Admin");
                }
                TempData["ErrorMessage"] = role;
                return View(login);

            }
            TempData["ErrorMessage"] = "Incorrect UserName";
            return View(login);
        }                                                                                                                               
        public IActionResult Index()
        {
            return View();
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
        public IActionResult GuestPage()
        {
            var Car = _carService.GetAll();
            return View(Car);
        }
    }
}
