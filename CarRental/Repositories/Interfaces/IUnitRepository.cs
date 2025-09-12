using CarRental.Models;

namespace CarRental.Repositories.Interfaces
{
    public interface IUnitRepository
    {
        

        bool CarExists(int carId);
        void Add(Unit unit);
        Task AddPlatesAsync(int carId, List<string> plates);
        IEnumerable<Unit> GetAll();
        Unit GetById(int id);
        void Update(Unit unit);
        void Delete(int id);
    }
}
