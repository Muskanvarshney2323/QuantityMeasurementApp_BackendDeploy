
using QuantityMeasurementAppModel.Entities;
using QuantityMeasurementAppRepositoryLayer.Context;
using QuantityMeasurementAppRepositoryLayer.Interfaces;

namespace QuantityMeasurementAppRepositoryLayer.Repositories
{
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        private readonly AppDbContext _context;

        public QuantityMeasurementDatabaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public void SaveMeasurementRecord(MeasurementRecord record)
        {
            _context.MeasurementRecords.Add(record);
            _context.SaveChanges();
        }

        public List<MeasurementRecord> GetAllRecords()
        {
            return _context.MeasurementRecords
                .OrderByDescending(x => x.Timestamp)
                .ToList();
        }

        public int GetRecordCount()
        {
            return _context.MeasurementRecords.Count();
        }
    }
}
