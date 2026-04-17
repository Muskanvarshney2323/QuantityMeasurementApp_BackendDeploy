using QuantityMeasurementAppModel.Enums;

namespace QuantityMeasurementAppModel.Entities
{
    public class MeasurementRecord
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime Timestamp { get; set; }

        public OperationType Operation { get; set; }

        public double? Input1Value { get; set; }
        public string? Input1Unit { get; set; }
        public string? Input1Type { get; set; }

        public double? Input2Value { get; set; }
        public string? Input2Unit { get; set; }
        public string? Input2Type { get; set; }

        public string? DesiredUnit { get; set; }

        public double? OriginalValue { get; set; }
        public string? OriginalUnit { get; set; }
        public string? OriginalType { get; set; }

        public double? OutputValue { get; set; }
        public string? OutputUnit { get; set; }
        public string? OutputText { get; set; }

        public bool SuccessFlag { get; set; }
        public string? ErrorMessage { get; set; }

    }
}