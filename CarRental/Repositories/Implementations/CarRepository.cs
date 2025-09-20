using CarRental.Data;
using CarRental.Models;
using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.repo.Implementations
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            return car.CarID;
        }
        IEnumerable<Car> ICarRepository.GetAll()
        {
            var cars = _context.Cars
                        .Where(c => !c.IsDeleted && !c.Brand.IsDeleted)
                        .Include(c => c.Brand)
                        .Include(c => c.Images)
                        .Include(c => c.Units)
                        .ToList();

            // Manually filter out deleted images and units
            foreach (var car in cars)
            {
                car.Images = car.Images?.Where(img => !img.IsDeleted).ToList();
                car.Units = car.Units?.Where(unit => !unit.IsDeleted).ToList();
            }

            return cars;
        }
        public List<int> GetCarIdsWithavailableUnits()
        {
            var carIds = _context.Units
                .Where(u => !u.IsDeleted && u.IsAvailble)
                .Select(u => u.CarID)
                .Distinct()
                .ToList();

            return carIds;
        }
        //
        IEnumerable<Car> ICarRepository.GetCarsByCarIds(List<int> CarId)
        {
            var cars = _context.Cars
                        .Where(c => !c.IsDeleted
                                    && !c.Brand.IsDeleted
                                    && CarId.Contains(c.CarID)) // <-- Filter by userIds
                        .Include(c => c.Brand)
                        .Include(c => c.Images)
                        .Include(c => c.Units /*&& c.IsAvailble*/)
                        .ToList();

            foreach (var car in cars)
            {
                car.Images = car.Images?.Where(img => !img.IsDeleted).ToList();
                car.Units = car.Units?.Where(unit => !unit.IsDeleted).ToList();
            }

            return cars;
        }



        public Car GetByID(int id)
        {
            var car = _context.Cars
                .Where(c => c.CarID == id && !c.IsDeleted && !c.Brand.IsDeleted)
                .Include(c => c.Brand)
                .Include(c => c.Images)
                .Include(c => c.Units)
                .FirstOrDefault();

            if (car != null)
            {
                car.Images = car.Images?.Where(img => !img.IsDeleted).ToList();
                car.Units = car.Units?.Where(unit => !unit.IsDeleted).ToList();
            }

            return car;
        }

        public string Update(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
            return "Car update successfully!";
        }
        //public List<Unit> GetUnitsByCarId(int carId)
        //{
        //    var units = _context.Units
        //        .Where(u => !u.IsDeleted && u.IsAvailble && u.CarID == carId)
        //        .Select(u => new Unit
        //        {
        //            UnitID = u.UnitID,               // ✅ Include UnitID here too
        //            CarID = u.CarID,
        //            PlateNumber = u.PlateNumber
        //        })
        //        .ToList();

        //    return units;
        //}



    }
}
