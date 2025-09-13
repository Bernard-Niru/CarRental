
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;


namespace CarRental.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;

        public BookingService(IBookingRepository repo)
        {
            _repo = repo;
        }
    //    public AddBooking
    }
}
