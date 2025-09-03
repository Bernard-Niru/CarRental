using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.repo.Interfaces
{
    public interface ICarRepository
    {
        public void AddCar(Car car);
        IEnumerable<Car> GetAll();

    }
}
