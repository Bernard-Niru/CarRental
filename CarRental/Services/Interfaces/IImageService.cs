
﻿using CarRental.DTOs;
using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces
{
    public interface IImageService
    {
        Task AddImagesAsync(AddImageViewModel model);
        Task<List<ImageDTO>> GetImagesForCarAsync(int carId);
        //void Add(Image image);
        //IEnumerable<Image> GetAll(); 
        //Image GetById(int id);
        //void Delete(int id);
        IEnumerable<ImageViewModel> GetImgsByCarID(int carID);//bn

    }
}
