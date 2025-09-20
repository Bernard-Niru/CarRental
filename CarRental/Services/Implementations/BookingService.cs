
using CarRental.DTOs;
using CarRental.Models;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;


namespace CarRental.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;

        public BookingService(IBookingRepository repo)
        {
            _repo = repo;
        }
        public void AddBooking(UnitSelectionViewModel request) 
        {
            var booking = new Booking
            {
                Unit = request.PlateNumber,
                RequestID = request.RequestId,
            };
        _repo.AddBooking(booking);
        }
        public IEnumerable<BookingDTO> GetAll()
        {
            var bookings = _repo.GetAll(); // Already filtered at repo

            var bookingDTOs = bookings.Select(booking => new BookingDTO
            {
                BookingID = booking.BookingID,
                RequestID = booking.RequestID,


                Request = new RequestDTO
                {
                    RequestID = booking.Request.RequestID,
                    UserID = booking.Request.UserID,
                    Username = booking.Request.User?.UserName,     // Safe if User is null
                    CarID = booking.Request.CarID,
                    CarName = booking.Request.Car?.CarName,        // Safe if Car is null
                    PickupDate = booking.Request.PickupDate,
                    PickupTime = booking.Request.PickupTime,
                    ReturnDate = booking.Request.ReturnDate,
                    ReturnTime = booking.Request.ReturnTime,
                }




            });

            return bookingDTOs;
        }
    }
}
