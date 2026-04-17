using System;

namespace QuantityMeasurementAppModel.Exceptions
{
    public class QuantityMeasurementException : Exception
    {
        public QuantityMeasurementException(string message) : base(message)
        {
        }
    }
}