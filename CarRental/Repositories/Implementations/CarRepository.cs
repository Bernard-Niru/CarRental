using CarRental.Data;
using CarRental.Models;
using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;

namespace CarRental.repo.Implementations
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public string AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            return "Car added successfully!";
        }
        IEnumerable<Car> ICarRepository.GetAll()
        {
            var cars = _context.Cars.
                        Where(c => !c.IsDeleted)
                        .ToList();

            return cars;
        }      
        public Car GetByID(int id)
        {
            return _context.Cars.Find(id);
        }
        public string Update(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
            return "Car update successfully!";
        }
    }
}
