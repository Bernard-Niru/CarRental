using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _service;

        public BrandController(IBrandService service)
        {
            _service = service; 
        }
    
        public IActionResult Index()
        {
            var brand = _service.GetAll();
            return View(brand);
        }
        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(BrandViewModel model) 
        {
            _service.Add(model);
            return RedirectToAction("Index");
        }
    }
}
