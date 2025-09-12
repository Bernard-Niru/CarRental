namespace CarRental.DTOs
{
    public class UnitDTO
    {
        public int UnitID { get; set; }
        public int CarID { get; set; }
        public string PlateNumber { get; set; }
        public string ImageBase64 { get; set; }
        public string ImageMimeType { get; set; }  
    }

}
