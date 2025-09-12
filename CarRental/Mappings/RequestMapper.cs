using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Mappings
{
    public class RequestMapper
    {
        public static Request ToModel(RequestViewModel model)
        {
            return new Request
            {
                RequestID = model.RequestID,
                UserID = model.UserID,
                CarID = model.CarID,
                PickupDate = model.PickupDate,
                PickupTime = model.PickupTime,
                ReturnDate = model.ReturnDate,
                ReturnTime = model.ReturnTime,
                IsAccepted = false,
                IsRejected = false,
                
            };
        }
    }
}
