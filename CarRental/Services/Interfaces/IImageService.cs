using CarRental.DTOs;
using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces
{
    public interface IImageService
    {
        Task AddImagesAsync(AddImageViewModel model);
        Task<List<ImageDTO>> GetImagesForCarAsync(int carId);
    }
}
