using CarRental.DTOs;

namespace CarRental.Services.Interfaces
{
    public interface IBookingService
    {
        void AddBooking(int id);
        IEnumerable<BookingDTO> GetAll();
    }
}