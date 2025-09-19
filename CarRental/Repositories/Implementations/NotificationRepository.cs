using CarRental.Data;
using CarRental.Models;
using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repositories.Implementations
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context; 
        }
        IEnumerable<Notification> INotificationRepository.GetAll(int id)
        {
            var notifications = _context.Notifications
                        .Where(n => n.UserID == id)
                        .Include(n => n.Car)
                        .OrderByDescending(n => n.DateTime)  // Order newest to oldest
                        .ToList();

            return notifications;
        }

        public void Add(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }
    }
}
