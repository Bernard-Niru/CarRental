using CarRental.Models;
using CarRental.Services.Implementations;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICarService _carService;
        private readonly IRequestService _requestService;

        public CustomerController(ICarService carService , IRequestService requestService)
        {
            _carService = carService;
            _requestService = requestService;
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
            int id = Session.UserID;
            model.UserID = id;
            _requestService.Add(model);
            return RedirectToAction("ViewPage");
        }

    }
}
