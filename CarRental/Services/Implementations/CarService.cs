using CarRental.repo.Interfaces;
using CarRental.Services.Interfaces;

namespace CarRental.Services.Implementations
{
    public class CarService : ICarService
    {
        public CarService(ICarRepository _repo) 
        {
            _repo = repo; 
        }
    }
}
