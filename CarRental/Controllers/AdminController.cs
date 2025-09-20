using CarRental.Enums.CarEnums;
using CarRental.Enums.UserEnums;
using CarRental.DTOs;
using CarRental.Models;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azure.Core;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using CarRental.Services.Implementations;

namespace CarRental.Controllers
{//Unit delete -1
    public class AdminController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly ICarService _carService;
        private readonly IImageService _imageService;
        private readonly IUnitService _unitService;
        private readonly IUserService _userService;
        private readonly IBookingService _bookingService;
        private readonly IRequestService _requestService;
        private readonly INotificationService _notificationService;
        private readonly IDashboardService _dashboardService;

        public AdminController(IBrandService brandService,
                       ICarService carService,    
                       IImageService imageService,
                       IUnitService unitService,
                       IUserService userService,
                       IBookingService bookingService,
                       IRequestService requestService,
            INotificationService notificationservice,
            IDashboardService dashboardService)

        {
            _brandService = brandService;
            _carService = carService;
            _imageService = imageService;
            _unitService = unitService;
            _userService = userService;
            _bookingService = bookingService;
            _requestService = requestService;
            _notificationService = notificationservice;
            _dashboardService = dashboardService;
        }


        //public async Task<IActionResult> Dashboard()
        //{
        //    var vm = new AdminDashboardViewModel
        //    {
        //        TotalRequests = _requestService.GetAll().Count(),
        //        TotalBookings = _bookingService.GetAll().Count(),
        //        PickedBookings = _bookingService.GetAllPicked().Count(),
        //        ReturnedBookings = _bookingService.GetAllReturned().Count(),
        //        NewUsersThisMonth = await _userService.GetNewUsersThisMonthAsync(),
        //        ActiveCustomers = await _userService.GetActiveCustomersAsync()
        //    };

        //    return View("AdminDashborad/Dashboard",vm);
        //}
        public async Task<IActionResult> Dashboard()
        {
            var vm = await _dashboardService.GetDashboardDataAsync();
            return View("AdminDashboard/Dashboard", vm);
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
            return View("Brand/_AddBrandPartial");
        }


        [HttpPost]
        public IActionResult AddBrand(BrandViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
          
            _brandService.Add(model);
            TempData["SuccessMessage"] = "Brand added successfully!";
            return RedirectToAction("AddCar", "Admin");

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
            TempData["SuccessMessage"] = "Brand deleted successfully!";
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

                TempData["SuccessMessage"] = "User added successfully!";
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
        public IActionResult EditUser(int Id)
        {
            var user = _userService.GetbyId(Id);
            ViewBag.RoleList = new SelectList(Enum.GetValues(typeof(UserRole)));
            return View("User/Edit", user);
        }
        [HttpPost]
        public IActionResult EditUser(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                _userService.Edit(user);
                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction("ViewUser");
            }
            return View(user);
        }
        public IActionResult DeleteUser(int Id)
        {
            _userService.Delete(Id);
            TempData["SuccessMessage"] = "User deleted successfully!";
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

            return View("Car/AddCar", new CarViewModel());
        }
        [HttpPost]
        public IActionResult AddCar(CarViewModel model)
        {
            ViewBag.CarType = new SelectList(Enum.GetValues(typeof(CarType)));
            ViewBag.FuelType = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.GearType = new SelectList(Enum.GetValues(typeof(GearType)));

            var brands = _brandService.GetAll()
                            .Select(b => new SelectListItem
                            {
                                Value = b.BrandID.ToString(),
                                Text = b.BrandName
                            }).ToList();
            ViewBag.BrandList = brands;

            if (ModelState.IsValid)
            {
                int newCarId = _carService.AddCar(model);
                TempData["SuccessMessage"] = "Car added successfully! You can now upload Images and Units.";

                model.CarID = newCarId;

                // ✅ Return the view using the correct subfolder path
                return View("Car/AddCar", model);
            }

            return View("Car/AddCar", model);
        }



        public IActionResult ViewCars()
        {
            var Car = _carService.GetAll();
            return View("Car/ViewCars", Car);
        }
        [HttpGet]
        public IActionResult UpdateCar(int id)
        {

            var car = _carService.GetByID(id);
            if (car == null)
            {
                return NotFound();
            }

           var brands = _brandService.GetAll()
            .Select(b => new SelectListItem
            {
                Value = b.BrandID.ToString(),
                Text = b.BrandName
            })
            .ToList();

            ViewBag.BrandList = brands;
            ViewBag.CarType = new SelectList(Enum.GetValues(typeof(CarType)));
            ViewBag.FuelType = new SelectList(Enum.GetValues(typeof(FuelType)));
            ViewBag.GearType = new SelectList(Enum.GetValues(typeof(GearType)));

            return View("Car/UpdateCar", car);
        }

        [HttpPost]
        public IActionResult UpdateCar(CarDTO model)
        {
            string message = _carService.Update(model);
            TempData["SuccessMessage"] = message;
            return RedirectToAction("ViewCars"); 
        }


        public IActionResult DeleteCar(int id)
        {

            _carService.Delete(id);
            TempData["SuccessMessage"] = "Car deleted successfully!";
            return RedirectToAction("ViewCars");
        }























        //=========================================== UNITS + IMAGES ======================================================


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

        //[HttpPost]
        //public async Task<IActionResult> AddUnit(AddUnitsViewModel model)
        //{
        //    if (ModelState.IsValid)
        //   {
        //        await _unitService.AddWithImagesAsync(model);
        //        return RedirectToAction("ViewUnits");
        //    }

        //    TempData["ErrorMessage"] = "Failed to add unit. Please check the form.";
        //    return RedirectToAction("ViewCars");
        //}


        //[HttpPost]
        //public async Task<IActionResult> AddUnit(AddUnitsViewModel model)
        //{
        //    foreach (var unit in model.Units)
        //    {
        //        foreach (var plate in unit.PlateNumbers)
        //        {
        //            if (plate.Length > 10)
        //            {
        //                ModelState.AddModelError("PlateNumbers", $"Plate number '{plate}' exceeds 10 characters.");
        //            }
        //        }
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        TempData["ErrorMessage"] = "Some plate numbers are invalid.";
        //        return RedirectToAction("ViewCars");
        //    }

        //    await _unitService.AddUnitsAsync(model);
        //    return RedirectToAction("ViewCars");
        //}


        [HttpPost]
        public async Task<IActionResult> UploadCarData(int CarID, List<IFormFile> ImageFiles, List<string> Units)
        {
            if ((ImageFiles == null || !ImageFiles.Any()) &&
                (Units == null || !Units.Any()))
            {
                TempData["ErrorMessage"] = "Please upload at least one image or unit.";
                return RedirectToAction("AddCar", new { id = CarID });
            }

            // Handle image uploads
            if (ImageFiles != null && ImageFiles.Any())
            {
                var images = new List<Image>();
                foreach (var file in ImageFiles)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);

                    images.Add(new Image
                    {
                        CarID = CarID,
                        ImageData = ms.ToArray(),
                        IsDeleted = false
                    });
                }

                _imageService.Add(images);
            }
         
                // Handle unit uploads
                if (Units != null && Units.Any())
            {
                var unitList = new List<Unit>();
                foreach (var unit in Units.Where(u => !string.IsNullOrWhiteSpace(u)))
                {
                    unitList.Add(new Unit
                    {
                        CarID = CarID,
                        PlateNumber = unit,
                        IsAvailble = true,
                        IsDeleted = false
                    });
                }
                var unitcount = _unitService.Add(unitList);
                _carService.ChangeUnitCount(CarID, unitcount);
                _carService.ChangeAvailableCount(CarID, unitcount);
            }

            TempData["SuccessMessage"] = "Data uploaded successfully!";
            return RedirectToAction("ViewCars");
        }



        public IActionResult ViewUnitofCar(int CarID) 
        {
            var units = _unitService.GetByCarID(CarID);
            ViewBag.CarID = CarID;
            return View("Car/UnitofCar",units);
        }
         
        public IActionResult ChangeAvailability(int id,int CarID) 
        {
            string message =_unitService.ChangeAvailability(id);
            int UnitCounts;
            if (message == "Add") {  UnitCounts = 1; }
            else if (message == "min") { UnitCounts = -1;  }
            else {  UnitCounts = 0; }
            _carService.ChangeAvailableCount(CarID, UnitCounts);

            return RedirectToAction("ViewUnitofCar", "Admin", new { CarID = CarID });
            
           
        }
        public IActionResult ViewImageofCar(int CarID)
        {
            var images = _imageService.GetByCarID(CarID);
            ViewBag.CarID = CarID;
            return View("Car/ImageofCar", images);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(int CarID, List<IFormFile> ImageFiles)
        {
            if (ImageFiles == null || !ImageFiles.Any())                 
            {
                TempData["ErrorMessage"] = "Please upload at least one image.";
                return RedirectToAction("ViewImageofCar", "Admin", new { CarID = CarID });
            }

            // Handle image uploads
            if (ImageFiles != null && ImageFiles.Any())
            {
                var images = new List<Image>();
                foreach (var file in ImageFiles)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);

                    images.Add(new Image
                    {
                        CarID = CarID,
                        ImageData = ms.ToArray(),
                        IsDeleted = false
                    });
                }

                _imageService.Add(images);
            }
            
            TempData["SuccessMessage"] = "Data uploaded successfully!";
            return RedirectToAction("ViewImageofCar", "Admin", new { CarID = CarID }); ;
        }

        [HttpPost]
        public async Task<IActionResult> AddUnits(int CarID, List<string> Units)
        {
            if (Units == null || !Units.Any())
            {
                TempData["ErrorMessage"] = "Please upload at least one unit.";
                return RedirectToAction("ViewUnitofCar", "Admin", new { CarID = CarID });
            }

            // Handle unit uploads
            if (Units != null && Units.Any())
            {
                var unitList = new List<Unit>();
                foreach (var unit in Units.Where(u => !string.IsNullOrWhiteSpace(u)))
                {
                    unitList.Add(new Unit
                    {
                        CarID = CarID,
                        PlateNumber = unit,
                        IsAvailble = true,
                        IsDeleted = false
                    });
                }
                var unitcount = _unitService.Add(unitList);
                _carService.ChangeUnitCount(CarID, unitcount);
                _carService.ChangeAvailableCount(CarID, unitcount);
            }

            TempData["SuccessMessage"] = "Data uploaded successfully!";
            return RedirectToAction("ViewUnitofCar", "Admin", new { CarID = CarID }); ;
        }

        public IActionResult DeleteImage(int id , int CarID)
        {

            _imageService.Delete(id);
            TempData["SuccessMessage"] = "Image deleted successfully!";
            return RedirectToAction("ViewImageofCar", "Admin", new { CarID = CarID });
        }
        public IActionResult DeleteUnit(int id, int CarID)
        {

            _unitService.Delete(id);
            TempData["SuccessMessage"] = "Unit deleted successfully!";
            return RedirectToAction("ViewUnitofCar", "Admin", new { CarID = CarID });
        }




















        //=============================================================== REQUESTS ======================================================
        public IActionResult ViewRequests()
        {
            var request = _requestService.GetAll();
            return View("Request/ViewRequests", request);

        }


        public IActionResult AcceptRequest(int id , int CarID ,int UserID)
        {
            _requestService.AcceptRequest(id,CarID,UserID);
            _bookingService.AddBooking( id);
            _carService.ChangeAvailableCount( CarID, -1);

            return RedirectToAction("ViewRequests");
        }

        public IActionResult RejectRequest(int id, int CarID, int UserID)

        {
            _requestService.RejectRequest(id,CarID,UserID);
            return RedirectToAction("ViewRequests");
        }




























        //=============================================================== BOOKINGS ======================================================
        public IActionResult ViewBookings()
        {
            var booking = _bookingService.GetAll();
            return View("Booking/ViewBookings",booking);

        }
        
        public IActionResult DeleteBooking(int id,int CarID,int UserID)
        {
            _bookingService.Delete(id, CarID, UserID);
            return RedirectToAction("ViewBookings");
        }

        public IActionResult ActiveRentals()
        {
            var conditions = Enum.GetValues(typeof(Condition))
                             .Cast<Condition>()
                             .Select(c => new SelectListItem
                             {
                                 Value = c.ToString(),
                                 Text = c.GetType()
                                         .GetMember(c.ToString())
                                         .First()
                                         .GetCustomAttribute<DisplayAttribute>()?
                                         .GetName() ?? c.ToString()
                             }).ToList();

            ViewBag.Condition = new SelectList(conditions, "Value", "Text");

            var booking = _bookingService.GetAllPicked();
            return View("Booking/ActiveRentals", booking);

        }

        public IActionResult Returned(BookingDTO bookingDTO)
        {
            ViewBag.Condition = new SelectList(Enum.GetValues(typeof(Condition)));
            _bookingService.Returned(bookingDTO);
            return RedirectToAction("ActiveRentals");
        }

        public IActionResult RentalHistory() 
        {
            ViewBag.Condition = new SelectList(Enum.GetValues(typeof(Condition)));
            var booking = _bookingService.GetAllReturned();
            return View("Booking/RentalHistory", booking);
        }

        public IActionResult PickupDelay(int UserID, int CarID)
        {
            _notificationService.Add(CarID, UserID, Purpose.PickupDelay);
            return RedirectToAction("ViewBookings");
        }

        public IActionResult ReturnDelay(int UserID, int CarID)
        {
            _notificationService.Add(CarID, UserID, Purpose.ReturnDelay);
            return RedirectToAction("ActiveRentals");
        }

        [HttpGet]
        public IActionResult Numberplat(int Bookingid, int CarID)
        {
            var units = _unitService.GetUnit(CarID).Select(b => new SelectListItem
            {
                Value = b.UnitID.ToString(),
                Text = b.PlateNumber
            }).ToList(); 
            ViewBag.Units = units;
            if (units == null) return RedirectToAction("ActiveRentals");
            var Booking = _bookingService.GetAll();
            var booking = new UnitSelectionViewModel
            {
                BookingDetails = Booking,
                BookingId = Bookingid,
                CarId = CarID
            };
            TempData["ShowModal"] = "open";
            return RedirectToAction("ViewBookings");
        }

        [HttpPost]
        public IActionResult NumberPlate(int BookingID, int SelectedUnitID, string PlateNumber, int CarID)
        {
            if(CarID < 0) return RedirectToAction("ViewBookings");
            _bookingService.PickedUp(BookingID, PlateNumber);
            _carService.ChangeAvailableCount(CarID, -1);
            _unitService.ChangeAvailability(SelectedUnitID);

            return RedirectToAction("ActiveRentals");
        }



    }





}

