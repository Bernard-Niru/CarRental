using CarRental.DTOs;
using CarRental.Enums.CarEnums;

namespace CarRental.DTOs 
{ 

    public class CarDTO
    {
        public int CarID { get; set; }
        public string CarName { get; set; }
        public int BrandID { get; set; }

        public CarType CarType { get; set; }
        public FuelType FuelType { get; set; }
        public GearType GearType { get; set; }

        public string Color { get; set; }
        public int No_of_Seats { get; set; }
        public double Ratings { get; set; }
        public decimal RentalRate { get; set; }
        public int UnitCount {  get; set; }
        

        //public RatingDTO Rating { get; set; }
        public BrandDTO Brand { get; set; }
        public List<ImageDTO> Images { get; set; }
        public List<UnitDTO> Units { get; set; }
    }
}