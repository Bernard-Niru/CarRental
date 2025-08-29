using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            _brandService.Add(model);
            return RedirectToAction("ViewBrands");

        }
        //===================================================== USER ===========================================================
        public IActionResult AddUser()
        {
            return View();
        }
    }//Thivaharan
}
