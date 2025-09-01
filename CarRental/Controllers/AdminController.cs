
﻿using CarRental.Enums.CarEnums;
using CarRental.Enums.UserEnums;
﻿using CarRental.DTOs;
using CarRental.Models;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRental.Controllers
{
    public class AdminController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly ICarService _carService;
        private readonly IImageService _imageService;
        private readonly IUnitService _unitService;
        private readonly IUserService _userService;
        private readonly IBookingService _bookingService;
        private readonly IRequestService _requestService;

        public AdminController(IBrandService brandService,
                       ICarService carService,    
                       IImageService imageService,
                       IUnitService unitService,
                       IUserService userService,
                       IBookingService bookingService,
                       IRequestService requestService)

        {
            _brandService = brandService;
            _carService = carService;
            _imageService = imageService;
            _unitService = unitService;
            _userService = userService;
            _bookingService = bookingService;
            _requestService = requestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //=========================================== BRANDS ===============================================================

        public IActionResult ViewBrands()
        {
            var brand = _brandService.GetAll();

            return View("Brand/ViewBrands", brand); // <-- explicitly point to the subfolder

        }

        [HttpGet]
        public IActionResult AddBrand()
        {
            return View("Brand/AddBrand");
        }


        [HttpPost]
        public IActionResult AddBrand(BrandViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
          
            _brandService.Add(model);
            return RedirectToAction("ViewBrands");

        }
        //===================================================== USER ===========================================================
        public IActionResult AddUser()
        {
            return View();





        }

        //==================================================== Car =============================================================
        [HttpGet]
        public IActionResult AddCar()
        {
            ViewBag.CarType = new SelectList(Enum.GetValues(typeof(CarType)));
            ViewBag.FuelType = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.GearType = new SelectList(Enum.GetValues(typeof(GearType)));

            // Get all brands as SelectListItems for dropdown
            var brands = _brandService.GetAll()
                        .Select(b => new SelectListItem
                        {
                            Value = b.BrandID.ToString(),
                            Text = b.BrandName
                        })
                        .ToList();

            ViewBag.BrandList = brands;

            return View("Car/AddCar");
        }













































































        //=========================================== UNITS + IMAGES ======================================================

        //[HttpGet]
        //public IActionResult AddUnit()
        //{
        //    return View("Image/AddUnit");
        //}
        //[HttpPost]
        //public async Task<IActionResult> AddUnit(UnitImageViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _unitService.AddWithImageAsync(model);
        //        return RedirectToAction("ViewUnit");
        //    }

        //    return View(model);
        //}


        //public IActionResult ViewUnits()
        //{
        //    var units = _unitService.GetAll(); // Already returns UnitDTOs
        //    return View("Image/ViewUnit",units);
        //}
    }
}
