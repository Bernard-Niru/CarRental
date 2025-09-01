using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;

namespace CarRental.Services.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRespository _repo;

        public CarService(ICarRespository repo) 
        {
            _repo = repo; 
        }
       
    }
}

