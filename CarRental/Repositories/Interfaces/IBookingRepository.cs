using CarRental.Models;

namespace CarRental.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        void AddBooking(Booking booking);
        IEnumerable<Booking> GetAll();
    }
}
