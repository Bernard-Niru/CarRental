using CarRental.Models;

namespace CarRental.Repositories.Interfaces
{
    public interface IUnitRepository
    {
        

        bool CarExists(int carId);
        void Add(Unit unit);
        Task AddPlatesAsync(int carId, List<string> plates);
        IEnumerable<Unit> GetAll();
        List<Unit> GetByCarID(int carId);
        void Update(Unit unit);
        void Delete(int id);
        void ChangeAvailability(int id);
    }
}
