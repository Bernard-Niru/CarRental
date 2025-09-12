using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.repo.Interfaces
{
    public interface ICarRepository
    {

        string AddCar(Car car);

        IEnumerable<Car> GetAll();

        string Update(Car car);

        Car GetByID(int id);

    }
}
