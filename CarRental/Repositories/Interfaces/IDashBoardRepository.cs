using CarRental.ViewModels;

namespace CarRental.Repositories.Interfaces
{
    public interface IDashBoardRepository
    {
        Task<AdminDashboardViewModel> GetDashboardDataAsync();
    }
}
