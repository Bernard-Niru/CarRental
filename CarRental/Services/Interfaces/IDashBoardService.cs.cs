using CarRental.ViewModels;

namespace CarRental.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<AdminDashboardViewModel> GetDashboardDataAsync();
    }
}
