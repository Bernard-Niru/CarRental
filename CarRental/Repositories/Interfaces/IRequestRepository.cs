using CarRental.Models;

namespace CarRental.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        void Add(Request request);
        IEnumerable<Request> GetAll();
    }
}
