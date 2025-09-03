using CarRental.DTOs;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces;

public interface ICarService
{
    void AddCar(CarViewModel model);

    IEnumerable<CarDTO> GetAll();

}
