namespace QuantityMeasurementAppModel.DTOs
{
    public class CalculationRequestDto
    {
        public string Operation { get; set; } = string.Empty;
        public double Value1 { get; set; }
        public string Unit1 { get; set; } = string.Empty;
        public double Value2 { get; set; }
        public string Unit2 { get; set; } = string.Empty;
        public double Value { get; set; }
        public string FromUnit { get; set; } = string.Empty;
        public string ToUnit { get; set; } = string.Empty;
        public string QuantityType { get; set; } = string.Empty;
    }
}
