using CarRental.Mappings;
using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;

namespace CarRental.Services.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repo;

        public CarService(ICarRepository repo) 
        {
            _repo = repo; 
        }
        public void AddCar(CarViewModel model)
        {
            var car = CarMapper.ToModel(model);
            _repo.AddCar(car);

        }
    }
}
        

