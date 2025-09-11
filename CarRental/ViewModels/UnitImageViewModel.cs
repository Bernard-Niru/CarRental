using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CarRental.ViewModels
{
    public class AddUnitsViewModel
    {
        public List<UnitImageViewModel> Units { get; set; } = new List<UnitImageViewModel>();


        
    }

    public class UnitImageViewModel
    {
        [Required]
        public int CarID { get; set; }

        [Required]
        public List<string> PlateNumbers { get; set; } = new List<string>();

      
    }
}
