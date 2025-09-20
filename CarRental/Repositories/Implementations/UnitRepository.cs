using CarRental.Data;
using CarRental.Models;
using CarRental.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repositories.Implementations
{
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext _context;

        public UnitRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Unit unit)
        {
            _context.Units.Add(unit);
            _context.SaveChanges(); 
        }
        public bool CarExists(int carId)
        {
            return _context.Cars.Any(c => c.CarID == carId);
        }

        public async Task AddPlatesAsync(int carId, List<string> plates)
        {
            foreach (var plate in plates)
                _context.Units.Add(new Unit { CarID = carId, PlateNumber = plate });
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Unit> GetAll()
        {
            return _context.Units
                .Include(u => u.Car)
                .Where(u => !u.IsDeleted)
                .ToList();
        }

        public List<Unit> GetByCarID(int carId)
        {
            return _context.Units              
                .Where(u => u.CarID == carId && !u.IsDeleted)
                .ToList();
        }


        public void Update(Unit unit)
        {
            _context.Units.Update(unit);
            _context.
                SaveChanges();
        }

        public void Delete(int id)
        {
            var unit = _context.Units.FirstOrDefault(u => u.UnitID == id);
            if (unit != null)
            {
                unit.IsDeleted = true;
                _context.SaveChanges();
            }
        }
        public string ChangeAvailability(int id) 
        {
            var unit = _context.Units.FirstOrDefault(u =>u.UnitID == id);
            if (unit != null)
            {
                if (unit.IsAvailble == false)
                {
                    unit.IsAvailble = true;
                    _context.SaveChanges();
                    return "Add";
                }
                else
                {
                    unit.IsAvailble = false;
                    _context.SaveChanges();
                    return "min";
                }
            }
            return null;
                                            
        }
        public List<Unit> GetUnitsByCarId(int carId)
        {
            var units = _context.Units
                .Where(u => !u.IsDeleted && u.IsAvailble)
                .Select(u => new Unit
                {
                    UnitID = u.UnitID,
                    CarID = u.CarID,
                    PlateNumber = u.PlateNumber
                })
                .ToList();

            return units;
        }
        public bool CheckUnit(string unit) 
        {
            
            return _context.Units.Any(u => u.PlateNumber == unit);
            

        }
        //public List<uint> GetUnits()
        //{
        //    return _context.Units
        //    .Include(u => u.Bookings)
        //        .Where(u =>
        //         u.Bookings.Any(b =>
        //        !b.CarPicked &&
        //        b.PlateNumber != u.PlateNumber  
        //        )
        //        )
        //        .ToList();

        //}

    }
}
