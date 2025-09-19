using CarRental.DTOs;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces;

public interface ICarService
{

    //GuestPageViewModel GetTopRatedCars();
    GuestPageViewModel GetAvailableCar();

    int AddCar(CarViewModel model);

    IEnumerable<CarDTO> GetAll();

    string Update(CarViewModel model);

    void Delete(int id);

    CarDTO? GetByID(int id);
    //List<UnitDTO> GetUnit(int id);

    void AddRating(int rating, int CarId);
    void ChangeUnitCount(int Carid, int Count);
    void ChangeAvailableCount(int Carid, int Count);




}
