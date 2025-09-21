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
                        .Where(b => !b.IsDeleted  && !b.IsPicked && !b.IsReturned)
                        .Include(b => b.Request)
                        .Include(b => b.Request.Car)
                        .Include(b => b.Request.User)
                        .ToList();

            return bookings;
        }
        public Booking GetBookingByID(int id)
        {
            return _context.Bookings.Find(id);
        }
        public void Update(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }
        IEnumerable<Booking> IBookingRepository.GetAllPicked()
        {
            var bookings = _context.Bookings
                        .Where(b => !b.IsDeleted && b.IsPicked && !b.IsReturned)
                        .Include(b => b.Request)
                        .Include(b => b.Request.Car)
                        .Include(b => b.Request.User)
                        .ToList();

            return bookings;
        }
        IEnumerable<Booking> IBookingRepository.GetAllReturned()
        {
            var bookings = _context.Bookings
                        .Where(b => !b.IsDeleted && b.IsPicked && b.IsReturned)
                        .Include(b => b.Request)
                        .Include(b => b.Request.Car)
                        .Include(b => b.Request.User)
                        .ToList();

            return bookings;
        }
        public void Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
        }

        public async Task<int> GetActiveCustomersAsync()
        {
            var bookings = await _context.Bookings
             .Include(b => b.Request)
             .ThenInclude(r => r.User)
             .ToListAsync();

            return bookings.Select(b => b.Request.UserID).Distinct().Count();
        }
        IEnumerable<Booking> IBookingRepository.GetUserBookingHistory(int userId)
        {
            var bookings = _context.Bookings
                .Where(b =>
                    !b.IsDeleted &&
                    b.IsPicked &&
                    b.IsReturned &&
                    b.Request.UserID == userId)
                .Include(b => b.Request)
                .Include(b => b.Request.Car)
                .Include(b => b.Request.User)
                .ToList();

            return bookings;
        }


    }
}
