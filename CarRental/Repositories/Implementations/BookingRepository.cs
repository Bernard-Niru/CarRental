using CarRental.Data;
using CarRental.Repositories.Interfaces;

namespace CarRental.Repositories.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
    }
}
