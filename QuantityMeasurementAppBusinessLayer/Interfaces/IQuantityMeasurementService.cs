using QuantityMeasurementAppModel.Entities;

namespace QuantityMeasurementAppBusinessLayer.Interfaces
{
    public interface IQuantityMeasurementService
    {
        string Add(double value1, string unit1, double value2, string unit2, string quantityType);
        string Subtract(double value1, string unit1, double value2, string unit2, string quantityType);
        string Compare(double value1, string unit1, double value2, string unit2, string quantityType);
        string Divide(double value1, string unit1, double value2, string unit2, string quantityType);
        string Convert(double value, string fromUnit, string toUnit, string quantityType);

        List<MeasurementRecord> GetHistory();
        int GetCount();
        List<string> GetOperationTypes();
        List<string> GetMeasurementTypes();
    }
}