using CarRental.repo.Interfaces;
using CarRental.Services.Interfaces;

namespace CarRental.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRespository _repo;

        public UserService(IUserRespository repo) 
        {
            _repo = repo;
        }
    }
}

