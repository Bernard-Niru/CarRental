namespace CarRental.DTOs
{
    public class CarWithImagesDTO
    {
        public int CarID { get; set; }
        public string CarName { get; set; }
        

        public List<ImageDTO> Images { get; set; } = new List<ImageDTO>();
    }
}
