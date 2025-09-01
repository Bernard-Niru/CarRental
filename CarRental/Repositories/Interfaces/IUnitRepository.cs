using CarRental.Models;

namespace CarRental.Repositories.Interfaces
{
    public interface IUnitRepository
    {
        void Add(Unit unit);
        IEnumerable<Unit> GetAll();
        Unit GetById(int id);
        void Update(Unit unit);
        void Delete(int id);
    }
}
