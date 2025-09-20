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
    }

}

