using CarRental.Data;
using CarRental.Models;
using CarRental.Repositories.Interfaces;

namespace CarRental.Repositories.Implementations
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
