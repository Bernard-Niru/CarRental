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

        public Unit GetById(int id)
        {
            return _context.Units
                .Include(u => u.Car)
                .FirstOrDefault(u => u.UnitID == id && !u.IsDeleted);
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
    }
}
