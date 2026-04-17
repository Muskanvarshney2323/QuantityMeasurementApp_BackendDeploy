using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAppModel.Entities;
using QuantityMeasurementAppRepositoryLayer.Context;
using QuantityMeasurementAppRepositoryLayer.Interfaces;

namespace QuantityMeasurementAppRepositoryLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? GetUserByEmail(string email)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user: " + ex.Message);
            }
        }

        public void AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                string errorMessage = ex.Message;

                if (ex.InnerException != null)
                {
                    errorMessage += " | Inner: " + ex.InnerException.Message;
                }

                if (ex.InnerException?.InnerException != null)
                {
                    errorMessage += " | SQL Inner: " + ex.InnerException.InnerException.Message;
                }

                throw new Exception("DB Save Error: " + errorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("General Error: " + ex.Message);
            }
        }
    }
}