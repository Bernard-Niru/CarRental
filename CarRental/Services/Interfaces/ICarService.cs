using CarRental.DTOs;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces;

public interface ICarService
{
    int AddCar(CarViewModel model);

    IEnumerable<CarDTO> GetAll();

    string Update(CarViewModel model);

    void Delete(int id);

    CarDTO? GetByID(int id);


}
