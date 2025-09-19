using CarRental.Enums.UserEnums;
using System.ComponentModel.DataAnnotations;

namespace CarRental.DTOs
{
    public class NotificationDTO
    {
        public int ID { get; set; }

        public string CarName { get; set; }

        public Purpose purpose { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsViewed { get; set; }
    }
}
