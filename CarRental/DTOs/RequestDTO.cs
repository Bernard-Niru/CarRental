using System.ComponentModel.DataAnnotations;

namespace CarRental.DTOs
{
    public class RequestDTO
    {
        public int RequestID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public int CarID { get; set; }
        public string CarName { get; set; }
        public DateOnly PickupDate { get; set; }
        public TimeOnly PickupTime { get; set; }
        public DateOnly ReturnDate { get; set; }
        public TimeOnly ReturnTime { get; set; }

        public CarDTO Car { get; set; }
        public UserDTO User { get; set; }
    }
}
