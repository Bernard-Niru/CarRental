using CarRental.Data;
using CarRental.Models;
using CarRental.Repositories.Interfaces;

namespace CarRental.Repositories.Implementations
{
    public class UserRepository : IBrandRespository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Brand> GetAll()
        {
            return _context.Brands.ToList();
        }
    }
}
