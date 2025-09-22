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
        private readonly ICarService _carService;

        public NotificationService(INotificationRepository repo, ICarService carService )
        {
            _repo = repo;
            _carService = carService;
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

        public void AddRatings(int ratings ,int CarID) 
        {
            var OldRating = _repo.GetByCarID(CarID);
            double Finalratings;

            if (OldRating != null)
            {
                OldRating.TotalRaters += 1;
                OldRating.TotalStars += ratings;
                Finalratings =(double) OldRating.TotalStars/OldRating.TotalRaters;
                _repo.UpdateRatings(OldRating);

            }
            else
            {

                var NewRating = new Ratings
                {
                    CarID = CarID,
                    TotalRaters = 1,
                    TotalStars = ratings,
                };
                Finalratings = ratings;
                _repo.AddRatings(NewRating);
            }

            _carService.UpdateRatings(Finalratings,CarID);
        }


    }
}
