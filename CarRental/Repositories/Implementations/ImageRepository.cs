//using CarRental.Data;
//using CarRental.Models;
//using CarRental.Repositories.Interfaces;

//namespace CarRental.Repositories.Implementations
//{
//    public class ImageRepository : IImageRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public ImageRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public void Add(Image image)
//        {
//            _context.Images.Add(image);
//            _context.SaveChanges();
//        }

//        public async Task AddImagesAsync(int carId, List<byte[]> imageDataList)
//        {
//            foreach (var data in imageDataList)
//            {
//                _context.Images.Add(new Image { CarID = carId, ImageData = data, IsDeleted = false });
//            }
//            await _context.SaveChangesAsync();
//        }


//        public IEnumerable<Image> GetAll()
//        {
//            return _context.Images.Where(i => !i.IsDeleted).ToList();
//        }

//        public Image GetById(int id)
//        {
//            return _context.Images.FirstOrDefault(i => i.ImageID == id && !i.IsDeleted);
//        }

//        public void Delete(int id)
//        {
//            var image = _context.Images.FirstOrDefault(i => i.ImageID == id);
//            if (image != null)
//            {
//                image.IsDeleted = true;
//                _context.SaveChanges();
//            }
//        }
//    }
//}
using CarRental.Models;
using CarRental.Data;
using System.Linq;
using CarRental.Repositories.Interfaces;

public class ImageRepository : IImageRepository
{
    private readonly ApplicationDbContext _context;

    public ImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Image image)
    {
        _context.Images.Add(image);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IEnumerable<Image> GetByCarId(int carId)
    {
        return _context.Images
            .Where(i => i.CarID == carId && !i.IsDeleted)
            .ToList();
    }
    public IEnumerable<Image> GetAll()
    {
        return _context.Images.Where(i => !i.IsDeleted).ToList();
    }

    public void Delete(int id)
    {
        var image = _context.Images.FirstOrDefault(i => i.ImageID == id);
        if (image != null)
        {
            image.IsDeleted = true;
            _context.SaveChanges();
        }
        
    }
    public IEnumerable<Image> GetImgsByCarID(int carID)
        {
            return _context.Images.Where(img => img.CarID == carID).ToList();
        }
}
