using CarRental.DTOs;
using CarRental.Enums.UserEnums;

namespace CarRental.Services.Interfaces
{
    public interface INotificationService
    {
        IEnumerable<NotificationDTO> GetAll(int id);
        void Add(int CarID, int UserID, Purpose purpose);
        void AddRatings(int ratings, int CarID);
    }
}
