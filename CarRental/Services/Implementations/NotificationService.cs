using CarRental.DTOs;
using CarRental.Enums.UserEnums;
using CarRental.Models;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;

namespace CarRental.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;

        public NotificationService(INotificationRepository repo )
        {
            _repo = repo;
        }
        public IEnumerable<NotificationDTO> GetAll(int id)
        {
            var notifications = _repo.GetAll(id); // Already filtered at repo

            var DTOs = notifications.Select(notifications => new NotificationDTO
            {
                ID = notifications.ID,
                CarID = notifications.CarID,
                CarName = notifications.Car.CarName,
                purpose = notifications.purpose,
                DateTime = notifications.DateTime,
                IsViewed = notifications.IsViewed,
            });

            return DTOs;
        }
        public void Add(int CarID , int UserID, Purpose purpose) 
        {
            var notification = new Notification
            {
                UserID = UserID,
                CarID = CarID,
                purpose = purpose,
                DateTime = DateTime.Now,
                IsViewed = false,
            };

            _repo.Add(notification);
        }


    }
}
