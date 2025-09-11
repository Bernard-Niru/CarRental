namespace CarRental.DTOs
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ImageBase64 { get; set; } 
        public string ImageMimeType { get; set; }
    }
}
