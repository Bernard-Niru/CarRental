using CarRental.DTOs;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces
{
    public interface IBookingService
    {
        void AddBooking(UnitSelectionViewModel request);
        IEnumerable<BookingDTO> GetAll();
    }
}