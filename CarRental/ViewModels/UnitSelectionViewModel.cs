namespace CarRental.ViewModels
{
    public class UnitSelectionViewModel
    {
        public IEnumerable<CarRental.DTOs.UnitDTO> Units { get; set; }
        public int RequestId { get; set; }
    }
}
