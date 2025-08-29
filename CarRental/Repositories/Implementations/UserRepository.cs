using CarRental.Data;
using CarRental.Models;
using CarRental.repo.Interfaces;

namespace CarRental.repo.Implementations
{
    public class UserRepository : IUserRespository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       
    }
}
