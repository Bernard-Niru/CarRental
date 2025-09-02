 using CarRental.Data;
using CarRental.Models;
using CarRental.repo.Interfaces;

namespace CarRental.repo.Implementations
{    

    public class BrandRespository : IBrandRespository
    {
        private readonly ApplicationDbContext _context;

        public BrandRespository(ApplicationDbContext context) 
        {
            _context = context;
        }

        IEnumerable<Brand> IBrandRespository.GetAll()
        {
            var brands = _context.Brands.
                         Where(b => !b.IsDeleted)
                         .ToList();

            return brands;
        }
        public void Add(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public void Update(Brand brand)
        {
            _context.Brands.Update(brand);
            _context.SaveChanges();
        }

        public Brand GetBrandByID(int id)
        {
            return _context.Brands.Find(id);
        }
    }
}
