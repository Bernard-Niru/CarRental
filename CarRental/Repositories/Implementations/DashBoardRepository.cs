using CarRental.Data;
using CarRental.DTOs;
using CarRental.Repositories.Interfaces;
using CarRental.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repositories.Implementations
{
    public class DashboardRepository : IDashBoardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashboardViewModel> GetDashboardDataAsync()
        {
            // Load all requests and bookings with related data
            var requests = await _context.Requests
                .Include(r => r.Car)
                    .ThenInclude(c => c.Brand)
                .Include(r => r.Car.Images)
                .ToListAsync();

            var bookings = await _context.Bookings
                .Include(b => b.Request)
                    .ThenInclude(r => r.Car)
                        .ThenInclude(c => c.Brand)
                .Include(b => b.Request.Car.Images)
                .ToListAsync();

            // Last 6 months
            var last6Months = Enumerable.Range(0, 6)
                .Select(i => DateTime.Now.AddMonths(-i))
                .OrderBy(d => d)
                .ToList();

            var bookingLabels = last6Months.Select(d => d.ToString("MMM yyyy")).ToList();

            // Count bookings per month using ActualPickupDate
            var bookingCounts = last6Months
                .Select(d => bookings.Count(b => b.ActualPickupDate.Month == d.Month && b.ActualPickupDate.Year == d.Year))
                .ToList();

            // Top 5 cars by booking count
            var topCars = bookings
                .Where(b => b.Request != null && b.Request.Car != null)
                .GroupBy(b => b.Request.Car.CarName)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new CarDTO
                {
                    CarName = g.Key,
                    RentalRate = g.First().Request.Car.RentalRate,
                    Brand = new BrandDTO
                    {
                        BrandName = g.First().Request.Car.Brand.BrandName
                    },
                    Images = g.First().Request.Car.Images?
                        .Select(i => new ImageDTO
                        {
                            ImageBase64 = Convert.ToBase64String(i.ImageData)
                        }).Take(1).ToList()
                }).ToList();

            // Requests status breakdown using boolean flags
            var accepted = requests.Count(r => r.IsAccepted);
            var rejected = requests.Count(r => r.IsRejected);
            var cancelled = requests.Count(r => !r.IsAccepted && !r.IsRejected);

            // Revenue per month using ActualPickupDate
            var monthlyRevenue = last6Months
                .Select(d => bookings
                    .Where(b => b.ActualPickupDate.Month == d.Month && b.ActualPickupDate.Year == d.Year)
                    .Sum(b => b.Request.Car.RentalRate))
                .ToList();

            // Dashboard view model
            return new AdminDashboardViewModel
            {
                TotalRequests = requests.Count,
                TotalBookings = bookings.Count,
                PickedBookings = bookings.Count(b => b.IsPicked),
                ReturnedBookings = bookings.Count(b => b.IsReturned),
                Labels = bookingLabels,
                BookingCounts = bookingCounts,
                TopCars = topCars,
                AcceptedRequests = accepted,
                RejectedRequests = rejected,
                CancelledRequests = cancelled,
                MonthlyRevenue = monthlyRevenue,
                TotalRevenue = monthlyRevenue.Sum()
            };
        }
    }
}
