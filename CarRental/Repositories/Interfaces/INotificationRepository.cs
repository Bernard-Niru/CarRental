using CarRental.Models;

namespace CarRental.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetAll(int id);
        void Add(Notification notification);
    }
}
