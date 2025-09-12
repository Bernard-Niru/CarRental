using CarRental.DTOs;
using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces
{
    public interface IRequestService
    {
        void Add(RequestViewModel model);
        IEnumerable<RequestDTO> GetAll();
    }
}
