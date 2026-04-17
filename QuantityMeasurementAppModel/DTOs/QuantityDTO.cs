namespace QuantityMeasurementAppModel.DTOs
{
    public class QuantityDTO
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}