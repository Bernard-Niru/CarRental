using CarRental.DTOs;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces;

public interface ICarService
{
    GuestPageViewModel GetGroupedCars();

    string AddCar(CarViewModel model);

    IEnumerable<CarDTO> GetAll();

    string Update(CarViewModel model);

    void Delete(int id);

    CarViewModel GetcarByID(int id);


}
