using CarRental.Models;

namespace CarRental.Repositories.Interfaces
{
    public interface IBrandRespository
    {
        IEnumerable<Brand> GetAll();
    }
}
