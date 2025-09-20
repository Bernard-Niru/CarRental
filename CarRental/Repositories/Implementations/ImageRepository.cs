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
        _context.SaveChanges();
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
        var image = _context.Images.Find(id);
        if (image != null)
        {
            _context.Images.Remove(image);
            _context.SaveChanges();
        }
    }
    public IEnumerable<Image> GetImgsByCarID(int carID)
        {
            return _context.Images.Where(img => img.CarID == carID).ToList();
        }
}
