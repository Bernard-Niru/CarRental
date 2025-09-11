using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICarService _carService;

        public CustomerController(ICarService carService)
        {
            _carService = carService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewPage()
        {
            var Car = _carService.GetAll();
            return View(Car);
        }

        public IActionResult Request() 
        {
            return View();
        }
    }
}
