
﻿//using CarRental.Mappings;
//using CarRental.Models;
//using CarRental.Repositories.Interfaces;
//using CarRental.Services.Interfaces;
//using CarRental.ViewModels;
//using System.IO;

//namespace CarRental.Services.Implementations
//{
//    public class ImageService : IImageService
//    {
//        private readonly IImageRepository _repo;

//        public ImageService(IImageRepository repo)
//        {
//            _repo = repo;
//        }

//        public async Task AddWithImagesAsync(AddImageViewModel model)
//        {
//            if (model == null || model.ImageFiles == null || !model.ImageFiles.Any())
//                return;

//            foreach (var file in model.ImageFiles)
//            {
//                if (file == null || file.Length == 0)
//                    continue;

//                if (!file.ContentType.StartsWith("image/"))
//                    continue;

//                try
//                {
//                    using var ms = new MemoryStream();
//                    await file.CopyToAsync(ms);
//                    var imageData = ms.ToArray();

//                    var image = new Image
//                    {
//                        CarID = model.CarID,
//                        ImageData = imageData,
//                        MimeType = file.ContentType,
//                        IsDeleted = false
//                    };

//                    _repo.Add(image);
//                }
//                catch (Exception ex)
//                {
//                    // Log error here
//                }
//            }
//        }




//    }
//}
using CarRental.ViewModels;
using CarRental.Models;
using CarRental.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using CarRental.Mappings;
using System.Linq;
using CarRental.DTOs;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepo;

    public ImageService(IImageRepository imageRepo)
    {
        _imageRepo = imageRepo;
    }

    public async Task AddImagesAsync(AddImageViewModel model)
    {
        if (model == null || model.ImageFiles == null || !model.ImageFiles.Any())
            return;

        foreach (var file in model.ImageFiles)
        {
            if (file == null || file.Length == 0)
                continue;
            if (!file.ContentType.StartsWith("image/"))
                continue;

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var imageData = ms.ToArray();


            var image = new Image
            {
                CarID = model.CarID,
                ImageData = imageData,
                MimeType = file.ContentType,
                IsDeleted = false
            };

            _imageRepo.Add(image);
        }

        await _imageRepo.SaveChangesAsync();
    }

    public async Task<List<ImageDTO>> GetImagesForCarAsync(int carId)
    {
        // Could be synchronous; here making it async for consistency
        var images = _imageRepo.GetByCarId(carId);
        var dtos = images.Select(i => ImageMapping.ToDTO(i)).ToList();
        return await Task.FromResult(dtos);

      

        public IEnumerable<ImageViewModel> GetImgsByCarID(int carID)
        {
            var images =  _imageRepo.GetImgsByCarID(carID);

            if (images == null || !images.Any())
            {
                // Return empty list or null, depending on your preference
                return Enumerable.Empty<ImageViewModel>();
            }

            return ImageMapper.ToViewModelList(images);
        }
    }
}