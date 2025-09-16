using CarRental.Data;
using CarRental.Models;
using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repositories.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }
        IEnumerable<Booking> IBookingRepository.GetAll()
        {
            var bookings = _context.Bookings
                        .Where(b => !b.IsDeleted  && !b.IsPicked)
                        .Include(b => b.Request)
                        .Include(b => b.Request.Car)
                        .Include(b => b.Request.User)
                        .ToList();

            return bookings;
        }

    }
}
