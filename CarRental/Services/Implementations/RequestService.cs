using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;

namespace CarRental.Services.Implementations
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repo;

        public RequestService(IRequestRepository repo)
        {
            _repo = repo;
        }
    }
}
