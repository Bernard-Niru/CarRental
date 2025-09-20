
using CarRental.DTOs;
using CarRental.Enums.UserEnums;
using CarRental.Models;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;


namespace CarRental.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;
        private readonly INotificationService _notificationService;
        private readonly IUnitService _unitService;

        public BookingService(IBookingRepository repo , INotificationService notificationService, IUnitService unitService)
        {
            _repo = repo;
            _notificationService = notificationService;
            _unitService = unitService;
        }
        public void AddBooking(int id) 
        {
            var booking = new Booking
            {
                Unit = "ABC 0000",
                RequestID = id,
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
                UnitList = _unitService.GetUnit(booking.Request.CarID),

                Request = new RequestDTO
                {
                    RequestID = booking.Request.RequestID,
                    UserID = booking.Request.UserID,
                    Username = booking.Request.User?.UserName,     // Safe if User is null
                    CarID = booking.Request.CarID,
                    CarName = booking.Request.Car?.CarName,        // Safe if Car is null
                    RentalRate = booking.Request.Car.RentalRate,
                    PickupDate = booking.Request.PickupDate,
                    PickupTime = booking.Request.PickupTime,
                    ReturnDate = booking.Request.ReturnDate,
                    ReturnTime = booking.Request.ReturnTime,


                }




            }); 

            return bookingDTOs;
        }
        public void PickedUp(int id , string plateNumber) 
        {
            
                var booking = _repo.GetBookingByID(id);
                if (booking != null)
                {
                    booking.Unit = plateNumber;
                    booking.IsPicked = true;
                    booking.ActualPickupDate = DateOnly.FromDateTime(DateTime.Now);
                    booking.ActualPickupTime = TimeOnly.FromDateTime(DateTime.Now);

                    _repo.Update(booking); // reuse update method
                }
            
        }
        public IEnumerable<BookingDTO> GetAllPicked()
        {
            var bookings = _repo.GetAllPicked(); // Already filtered at repo

            var bookingDTOs = bookings.Select(booking => new BookingDTO
            {
                BookingID = booking.BookingID,
                RequestID = booking.RequestID,
                ActualPickupDate = booking.ActualPickupDate,
                ActualPickupTime = booking.ActualPickupTime,


                Request = new RequestDTO
                {
                    RequestID = booking.Request.RequestID,
                    UserID = booking.Request.UserID,
                    Username = booking.Request.User?.UserName,     // Safe if User is null
                    CarID = booking.Request.CarID,
                    CarName = booking.Request.Car?.CarName,        // Safe if Car is null
                    RentalRate = booking.Request.Car.RentalRate,
                    ReturnDate = booking.Request.ReturnDate,
                    ReturnTime = booking.Request.ReturnTime,
                }


            });

            return bookingDTOs;
        }
        public void Returned(BookingDTO bookingDTO)
        {

            var booking = _repo.GetBookingByID(bookingDTO.BookingID);
            if (booking != null)
            {
                booking.RentalAmount = (bookingDTO.RentalAmount + bookingDTO.AdditionalCharges) - bookingDTO.Discount;
                booking.Condition = bookingDTO.Condition;
                booking.IsReturned = true;
                booking.ActualReturnDate = DateOnly.FromDateTime(DateTime.Now);
                booking.ActualReturnTime = TimeOnly.FromDateTime(DateTime.Now);

                _repo.Update(booking); // reuse update method
                _notificationService.Add(bookingDTO.Request.CarID, bookingDTO.Request.UserID, Purpose.Feedback);
            }

        }
        public IEnumerable<BookingDTO> GetAllReturned()
        {
            var bookings = _repo.GetAllReturned(); // Already filtered at repo

            var bookingDTOs = bookings.Select(booking => new BookingDTO
            {
                BookingID = booking.BookingID,
                RequestID = booking.RequestID,
                ActualPickupDate = booking.ActualPickupDate,
                ActualPickupTime = booking.ActualPickupTime,
                ActualReturnDate = booking.ActualReturnDate,
                ActualReturnTime = booking.ActualReturnTime,
                RentalAmount = booking.RentalAmount,
                Condition = booking.Condition,
                Unit = booking.Unit,
                Ratings = booking.Ratings,


                Request = new RequestDTO
                {
                    RequestID = booking.Request.RequestID,
                    UserID = booking.Request.UserID,
                    Username = booking.Request.User?.UserName,     // Safe if User is null
                    CarID = booking.Request.CarID,
                    CarName = booking.Request.Car?.CarName,        // Safe if Car is null
                }


            });

            return bookingDTOs;
        }
        public void Delete(int id, int CarID, int UserID) 
        {
            _repo.Delete(id);
            _notificationService.Add(CarID, UserID, Purpose.BookingCancel);

        }
    }
}
