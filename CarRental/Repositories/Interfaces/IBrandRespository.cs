using CarRental.Models;

namespace CarRental.repo.Interfaces
{
    public interface IBrandRespository
    {
        IEnumerable<Brand> GetAll();

        void Add(Brand brand);

        void Update(Brand brand);

        Brand GetBrandByID(int id);


    }
}
