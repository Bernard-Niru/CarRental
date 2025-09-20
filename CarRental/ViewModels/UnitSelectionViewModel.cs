namespace CarRental.ViewModels
{
    public class UnitSelectionViewModel
    {
        public IEnumerable<CarRental.DTOs.BookingDTO> BookingDetails { get; set; }
        public int CarId { get; set; }
        public int BookingId { get; set; }
        public int UnitId { get; set; }
        public string PlatNumber { get; set; }
    }
}
