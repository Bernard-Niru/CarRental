using CarRental.DTOs;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces;

public interface ICarService
{

    //GuestPageViewModel GetTopRatedCars();
    GuestPageViewModel GetAvailableCar();

    int AddCar(CarViewModel model);

    IEnumerable<CarDTO> GetAll();

    string Update(CarDTO model);

    void Delete(int id);

    CarDTO? GetByID(int id);
    //List<UnitDTO> GetUnit(int id);

    void AddRating(int rating, int CarId);
    


    }
