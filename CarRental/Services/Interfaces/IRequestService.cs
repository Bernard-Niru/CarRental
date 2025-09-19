using CarRental.DTOs;
using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces
{
    public interface IRequestService
    {
        void Add(RequestViewModel model);
        IEnumerable<RequestDTO> GetAll();
        void AcceptRequest(int id, int CarID, int UserID);
        void RejectRequest(int id, int CarID, int UserID);


        }
}
