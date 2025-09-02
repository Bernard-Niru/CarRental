using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;

namespace CarRental.Services.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repo;

        public CarService(ICarRepository repo) 
        {
            _repo = repo; 
        }
       
    }
}

