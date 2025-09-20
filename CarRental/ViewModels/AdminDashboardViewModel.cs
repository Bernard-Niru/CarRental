using CarRental.DTOs;

namespace CarRental.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalRequests { get; set; }
        public int TotalBookings { get; set; }
        public int PickedBookings { get; set; }
        public int ReturnedBookings { get; set; }
        public int NewUsersThisMonth { get; set; }
        public int ActiveCustomers { get; set; }


        // Bookings trend
        public List<string> Labels { get; set; } = new();
        public List<int> BookingCounts { get; set; } = new();

        // Top rented cars
        public List<CarDTO> TopCars { get; set; } = new();

        // Requests breakdown
        public int AcceptedRequests { get; set; }
        public int RejectedRequests { get; set; }
        public int CancelledRequests { get; set; }

        // Revenue
        public decimal TotalRevenue { get; set; }
        public List<decimal> MonthlyRevenue { get; set; } = new();

    }

}

