using CarRental.Models;

namespace CarRental.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        void AddBooking(Booking booking);
        IEnumerable<Booking> GetAll();
        Booking GetBookingByID(int id);
        void Update(Booking booking);
        IEnumerable<Booking> GetAllPicked();
        IEnumerable<Booking> GetAllReturned();
        void Delete(int id);
        Task<int> GetActiveCustomersAsync();
        IEnumerable<Booking> GetUserBookingHistory(int userId);
    }
}
