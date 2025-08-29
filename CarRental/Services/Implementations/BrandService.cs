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
    }
}
