using CarRental.DTOs;

namespace CarRental.Services.Interfaces
{
    public interface IBookingService
    {
        void AddBooking(int id);
        IEnumerable<BookingDTO> GetAll();
        void PickedUp(int id, string plateNumber);
        IEnumerable<BookingDTO> GetAllPicked();
        void Returned(BookingDTO bookingDTO);
        IEnumerable<BookingDTO> GetAllReturned();
        void Delete(int id, int CarID, int UserID);
        IEnumerable<BookingDTO> GetUserBookingHistory(int userId);
    }
}