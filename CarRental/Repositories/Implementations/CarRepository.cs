using CarRental.Data;
using CarRental.Models;
using CarRental.repo.Interfaces;

namespace CarRental.repo.Implementations
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddCar(Car car) 
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }
    }
}
