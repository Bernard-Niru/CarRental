using CarRental.DTOs;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces;

public interface ICarService
{

    //GuestPageViewModel GetTopRatedCars();
    GuestPageViewModel GetAvailableCar();

    CustomerViewModel GetAvailableCarsForCustomer();
    int AddCar(CarViewModel model);

    IEnumerable<CarDTO> GetAll();

    string Update(CarDTO model);

    void Delete(int id);

    CarDTO? GetByID(int id);
    //List<UnitDTO> GetUnit(int id);

    void ChangeUnitCount(int Carid, int Count);
    void ChangeAvailableCount(int Carid, int Count);
    CarDTO Search(string carName, int BrandId, string color);




}
