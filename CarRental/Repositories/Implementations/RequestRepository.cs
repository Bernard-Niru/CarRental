using CarRental.Data;
using CarRental.Repositories.Interfaces;

namespace CarRental.Repositories.Implementations
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
    }
}
