using CarRental.DTOs;
using CarRental.Mappings;
using CarRental.Models;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CarRental.Services.Implementations
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repo;
        private readonly ICarService _carService;
        private readonly IUserService _userService;

        public RequestService(IRequestRepository repo, ICarService carService, IUserService userService)
        {
            _repo = repo;
            _carService = carService;
            _userService = userService;
        }

        public void Add(RequestViewModel model)
        {
                     
            var request = RequestMapper.ToModel(model);
            _repo.Add(request);
        }
        public IEnumerable<RequestDTO> GetAll()
        {
            var requests = _repo.GetAll();
            var requestlist = new List<RequestDTO>();

            foreach (var r in requests)
            {

                var model = new RequestDTO
                {
                    RequestID = r.RequestID,
                    UserID = r.UserID,
                    Username = _userService.GetbyId(r.UserID).UserName,
                    CarID = r.CarID,
                    CarName = _carService.GetcarByID(r.CarID).CarName,
                    PickupDate = r.PickupDate,
                    PickupTime = r.PickupTime,
                    ReturnDate = r.ReturnDate,
                    ReturnTime = r.ReturnTime,


                };

                requestlist.Add(model);
            }

            return requestlist;
        }
    }
}
