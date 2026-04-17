using System;

namespace QuantityMeasurementAppModel.DTOs
{
    public class MeasurementRecordDto
    {
        public int Id { get; set; }
        public string OperationType { get; set; } = string.Empty;
        public string InputData { get; set; } = string.Empty;
        public string ResultData { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}