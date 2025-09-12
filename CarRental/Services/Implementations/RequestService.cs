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

        public RequestService(IRequestRepository repo)
        {
            _repo = repo;
        }

        public void Add(RequestViewModel model)
        {
            
            model.UserID = 4;
            
           
            var request = RequestMapper.ToModel(model);
            _repo.Add(request);
        }
    }
}
