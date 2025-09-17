using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.repo.Interfaces
{
    public interface ICarRepository
    {

        public int AddCar(Car car);

        IEnumerable<Car> GetAll();

        string Update(Car car);

        Car GetByID(int id);
        //List<int> GetCarIdsWithavailableUnits();
        IEnumerable<Car> GetAvailableCars();
        //List<Unit> GetUnitsByCarId(int carId);
    }
}
