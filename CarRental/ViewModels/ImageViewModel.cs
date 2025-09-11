
﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace CarRental.ViewModels
{
    public class AddImageViewModel
    {

        [Key]
        public int ImageID { get; set; }

        [Required]
        public int CarID { get; set; }

        [Required]

        public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();//thiva
      
        public byte[] ImageData { get; set; }//bn

    }
}

