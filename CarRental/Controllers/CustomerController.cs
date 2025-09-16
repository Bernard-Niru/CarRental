using CarRental.Services.Implementations;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

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
            int id = int.Parse(Role.Id);
            model.UserID = id;
            _requestService.Add(model);
            return RedirectToAction("ViewPage");
        }




        public IActionResult Ratings(int rating)
        {
            int CarID = 2;
           _carService.AddRating(rating, CarID);
            return View("Ratings");
        }

    }
    
}
