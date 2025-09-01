using CarRental.Data;
using CarRental.Repositories.Interfaces;

namespace CarRental.Repositories.Implementations
{
    public class CarRespository : ICarRespository 
    {
        private readonly ApplicationDbContext _context;

        public CarRespository(ApplicationDbContext context)
        {
            _context = context;  
        }
    }
}