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
{
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
        

        private CombinedViewModel open(UserViewModel user)
        {
            var guestViewModel = _carService.GetAvailableCar();
            return new CombinedViewModel
            {
                GuestPage = guestViewModel,
                User = user ?? new UserViewModel(),
                Login = new LoginViewModels()
            };
        }



        [HttpGet]
        public IActionResult Login()
        {

            var combinedViewModel = open(null);
            TempData["LoginErrorMessage"] = "1";
            return View("Index" , combinedViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel User)
        {
            User.Role = Enums.UserEnums.UserRole.Customer;


                if (await _userservice.CheckEmailAsync(User.EmailAddress.Trim()))
                {
                    TempData["RegisterErrorMessage"] = "Email already exists";
                    var combinedViewModel = open(User);
                    return View("Index", combinedViewModel);
                }

                if (await _userservice.CheckAsync(User.UserName.Trim()))
                {
                    TempData["RegisterErrorMessage"] = "Username already exists";
                    TempData["ShowRegisterModal"] = true;
                    var combinedViewModel = open(User);
                    return View("Index", combinedViewModel);
                }

                await _userservice.AddAsync(User);
                return RedirectToAction("Login", "Home");
            }

        [HttpPost]
        public async Task<IActionResult> login(LoginViewModels login)
        {
            string username = login.Usernamelogin?.Trim();
            bool userExists = await _userservice.CheckAsync(username);

            if (userExists)
            {
                string role = _userservice.CheckPassword(login);

                if (role.Contains(','))
                {
                    string[] user = role.Split(',');
                    Session.Role = user[0].Trim();
                    Session.UserID = int.Parse(user[1].Trim());

                    if (user[0] == "Admin")
                        return RedirectToAction("Index", "Admin");
                    if (user[0] == "Customer")
                        return RedirectToAction("HomePage", "Customer");
                    if (user[0] == "Staff")
                        return RedirectToAction("ViewCars", "Admin");
                }
                TempData["LoginErrorMessage"] = role;
            }
            else
            {
                TempData["LoginErrorMessage"] = "Incorrect UserName";
            }
            var combinedViewModel = open(null);
            return View("Index", combinedViewModel);
        }

        [HttpGet]
        public IActionResult Index()
        {

            var combinedViewModel = open(null);
            return View(combinedViewModel);
        }

      
        public IActionResult Privacy()
        {
            TempData["LoginErrorMessage"] = "1";
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
