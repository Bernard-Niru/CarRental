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
                        .ToList();

            // Manually filter out deleted images and units


            return cars;
        }
        //public List<int> GetCarIdsWithavailableUnits()
        //{
        //    var carIds = _context.Units
        //        .Where(u => !u.IsDeleted && u.IsAvailble)
        //        .Select(u => u.CarID)
        //        .Distinct()
        //        .ToList();

        //    return carIds;
        //}

        IEnumerable<Car> ICarRepository.GetAvailableCars()
        {
                    var cars = _context.Cars
                                .Where(c => !c.IsDeleted
                                            && !c.Brand.IsDeleted
                                            && c.AvailableUnit > 0) 
                                .Include(c => c.Brand)
                                .Include(c => c.Images)
                                .Include(c => c.Units)
                                .ToList();

                    foreach (var car in cars)
                    {
                        car.Images = car.Images?.Where(img => !img.IsDeleted).ToList();
                        car.Units = car.Units?.Where(unit => !unit.IsDeleted && unit.IsAvailble).ToList(); // also filter units here
                    }

                    return cars;
                    ;
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

        public void AddUnitCount(int CarId,int UnitCount)
        {
            var AddCount = _context.Cars.FirstOrDefault(u => u.CarID == CarId);
            AddCount.UnitCount = UnitCount;
            _context.Cars.Update(AddCount);
            _context.SaveChanges();
        }

        public void AddAvailableUnitCount(int CarId, int UnitCount)
        {
            var AddCount = _context.Cars.FirstOrDefault(u => u.CarID == CarId);
            AddCount.AvailableUnit = UnitCount;
            _context.Cars.Update(AddCount);
            _context.SaveChanges();
        }

        public (int UnitCount, int AvailableUnit) GetCounts(int carId)
        {
            var result = _context.Cars
                .Where(c => c.CarID == carId && !c.IsDeleted)
                .Select(c => new
                {
                    c.UnitCount,
                    c.AvailableUnit
                })
                .FirstOrDefault();

            return (result.UnitCount, result.AvailableUnit);
        }

        public Car Search(string carName,int BrandId,string color)
        {
            var car = _context.Cars
                .Where(c => !c.IsDeleted &&
                !c.Brand.IsDeleted &&
                c.CarName.ToLower() == carName.ToLower() &&
                c.BrandID == BrandId&&
                c.Color == color)
                .Include(c => c.Brand)
                .Include(c => c.Images)
                .Include(c => c.Units)
                .FirstOrDefault();

             if(car!= null)   car.Images = car.Images?.ToList();
            if (car == null)return null;
            return car;
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
