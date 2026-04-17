using QuantityMeasurementAppModel.Entities;

namespace QuantityMeasurementAppRepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void AddUser(User user);
    }
}