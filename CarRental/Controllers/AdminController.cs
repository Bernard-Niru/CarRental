using CarRental.Enums.CarEnums;
using CarRental.Enums.UserEnums;
using CarRental.DTOs;
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

        
            public IActionResult Admin_Layout()
            {
                return View("~/Views/Shared/Admin_Layout.cshtml");
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
        [HttpGet]
        public IActionResult UpdateBrand(int id)
        {
            var brand = _brandService.GetBrandByID(id);
            if (brand == null)
            {
                TempData["ErrorMessage"] = "Brand Not Found";
                return RedirectToAction("ViewBrands");
            }
            return View("Brand/UpdateBrand", brand);
        }
        [HttpPost]
        public IActionResult UpdateBrand(BrandViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                _brandService.Update(model);
                return RedirectToAction("ViewBrands");
            }
            return View(model);
        }

        public IActionResult DeleteBrand(int id) 
        {
            
            _brandService.Delete(id);
            return RedirectToAction("ViewBrands");
        }

        //===================================================== USER ===========================================================
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            ViewBag.RoleList = new SelectList(Enum.GetValues(typeof(UserRole)));
            return View("User/AddUser");
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            ViewBag.RoleList = new SelectList(Enum.GetValues(typeof(UserRole)));

            if (ModelState.IsValid)
            {
                if (await _userService.CheckAsync(model.UserName.Trim()))
                {
                    TempData["ErrorMessage"] = "UserName already exists";
                    return View("User/AddUser", model);
                }

                await _userService.AddAsync(model);
                return RedirectToAction("ViewUser");
            }

            return View("User/AddUser", model);
        }

        public async Task<IActionResult> ViewUser()
        {
            var User = await _userService.GetallAsync();
            return View("User/ViewUser",User);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var user = _userService.GetbyId(Id);
            ViewBag.RoleList = new SelectList(Enum.GetValues(typeof(UserRole)));
            return View("User/Edit", user);
        }
        [HttpPost]
        public IActionResult Edit(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                _userService.Edit(user);
                return RedirectToAction("ViewUser");
            }
                return View(user);
        }
        public IActionResult Delete(int Id)
        {
            _userService.Delete(Id);
            return RedirectToAction("ViewUser");
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
        [HttpPost]
        public IActionResult AddCar(CarViewModel model)
        {
            ViewBag.RoleList = new SelectList(Enum.GetValues(typeof(CarType)));
            ViewBag.RoleList = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.RoleList = new SelectList(Enum.GetValues(typeof(GearType)));

            if (ModelState.IsValid) 
            {
                _carService.AddCar(model);
                return RedirectToAction("ViewCars");
            }
            return View("Car/AddCar",model);
        }

        public IActionResult ViewCars()
        {
            var Car = _carService.GetAll();
            return View("Car/ViewCars", Car);
        }












































































        //=========================================== UNITS + IMAGES ======================================================

        [HttpGet]
        public IActionResult AddUnit()
        {
            return View("Image/AddUnit");
        }
        //[HttpPost]
        //public async Task<IActionResult> AddUnit(UnitImageViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _unitService.AddWithImagesAsync(model);
        //        return RedirectToAction("ViewUnit");
        //    }

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddUnit(UnitImageViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Create an instance of the new ViewModel
        //        var unitsModel = new AddUnitsViewModel();
        //        unitsModel.Units.Add(model);

        //        // Call the corrected method on the service
        //        await _unitService.AddWithImagesAsync(unitsModel);

        //        return RedirectToAction("ViewUnits");
        //    }

        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> AddUnit(AddUnitsViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _unitService.AddWithImagesAsync(model);
                return RedirectToAction("ViewUnits");
            }

            TempData["ErrorMessage"] = "Failed to add unit. Please check the form.";
            return RedirectToAction("ViewCars");
        }




        public IActionResult ViewUnits()
        {
            var units = _unitService.GetAll();
            return View("Image/ViewUnit", units);
        }






    }
}
