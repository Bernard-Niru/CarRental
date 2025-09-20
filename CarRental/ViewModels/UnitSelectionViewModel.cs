namespace CarRental.ViewModels
{
    public class UnitSelectionViewModel
    {
        public IEnumerable<CarRental.DTOs.UnitDTO> Units { get; set; }
        public IEnumerable<CarRental.DTOs.RequestDTO> car {  get; set; }
        public int RequestId { get; set; }
        public string PlateNumber { get; set; }
        public int Unit {  get; set; }
    }
}
