using CarRental.Enums.CarEnums;
using System.ComponentModel.DataAnnotations;

namespace CarRental.DTOs
{
    public class BookingDTO
    {
        public int BookingID { get; set; }      
        public int RequestID { get; set; }
        public bool IsPicked { get; set; }
        public DateOnly ActualPickupDate { get; set; }
        public TimeOnly ActualPickupTime { get; set; }
        public bool IsReturned { get; set; }
        public DateOnly ActualReturnDate { get; set; }
        public TimeOnly ActualReturnTime { get; set; }
        public string Unit { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Rental amount must be positive")]
        public decimal RentalAmount { get; set; }
        public Condition Condition { get; set; }
        public double Ratings { get; set; }
        public decimal Discount { get; set; }
        public decimal AdditionalCharges { get; set; }
        

        public RequestDTO Request { get; set; }
        


    }
}
