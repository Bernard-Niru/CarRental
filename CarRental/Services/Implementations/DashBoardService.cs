using CarRental.Repositories.Implementations;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;

namespace CarRental.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashBoardRepository _dashboardRepo;
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;
        public DashboardService(
           IDashBoardRepository dashboardRepo,
            IBookingService bookingService,
            IUserService userService)
        {
            _dashboardRepo = dashboardRepo;
            _bookingService = bookingService;
            _userService = userService;
        }

        public async Task<AdminDashboardViewModel> GetDashboardDataAsync()
        {
            var vm = await _dashboardRepo.GetDashboardDataAsync();

            // Add service-specific data
            vm.PickedBookings = _bookingService.GetAllPicked().Count();
            vm.ReturnedBookings = _bookingService.GetAllReturned().Count();
            vm.NewUsersThisMonth = await _userService.GetNewUsersThisMonthAsync();
            vm.ActiveCustomers = await _userService.GetActiveCustomersAsync();

            return vm;
        }
    }
}
