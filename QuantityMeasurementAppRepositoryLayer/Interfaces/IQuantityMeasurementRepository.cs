
using QuantityMeasurementAppModel.Entities;

namespace QuantityMeasurementAppRepositoryLayer.Interfaces
{
    public interface IQuantityMeasurementRepository
    {
        void SaveMeasurementRecord(MeasurementRecord record);
        List<MeasurementRecord> GetAllRecords();
        int GetRecordCount();
    }
}
