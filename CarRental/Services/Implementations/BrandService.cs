using CarRental.DTOs;
using CarRental.Mappings;
using CarRental.repo.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;

namespace CarRental.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRespository _repo;

        public BrandService(IBrandRespository repo)
        {
            _repo = repo;
        }
        public IEnumerable<BrandDTO> GetAll()
        {
            var brands = _repo.GetAll();
            var brandDtos = new List<BrandDTO>();

            foreach (var b in brands)
            {
                var dto = new BrandDTO
                {
                    BrandID = b.BrandID,
                    BrandName = b.BrandName,
                    
                };
                brandDtos.Add(dto);
            }

            return brandDtos;
        }
        public void Add(BrandViewModel model) 
        {
            var brand = BrandMapper.ToModel(model);
            _repo.Add(brand);
        }

        public void Update(BrandViewModel model)
        {
            var brand = BrandMapper.ToModel(model);
            _repo.Update(brand);
        }

        public BrandViewModel GetBrandByID(int id)
        {
            var brand = _repo.GetBrandByID(id);
            if (brand == null)
            {
                // Could throw an exception or return null
                return null;
            }
            return BrandMapper.ToViewModel(brand);
        }
        public void Delete(int id)
        {
            var brand = _repo.GetBrandByID(id);
            if (brand != null)
            {
                brand.IsDeleted = true;
                _repo.Update(brand); // reuse update method
            }
        }

    }
}
