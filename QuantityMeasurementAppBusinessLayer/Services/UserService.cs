using QuantityMeasurementAppBusinessLayer.Interfaces;
using QuantityMeasurementAppModel.DTOs;
using QuantityMeasurementAppModel.Entities;
using QuantityMeasurementAppRepositoryLayer.Interfaces;

namespace QuantityMeasurementAppBusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public AuthResponseDto Register(RegisterDto dto)
        {
            var existingUser = _repository.GetUserByEmail(dto.Email);

            if (existingUser != null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "User already exists"
                };
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                IsGoogleUser = false
            };

            _repository.AddUser(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = "User registered successfully",
                Name = user.Name,
                Email = user.Email
            };
        }

        public AuthResponseDto Login(LoginDto dto)
        {
            var user = _repository.GetUserByEmail(dto.Email);

            if (user == null || user.PasswordHash != dto.Password)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid credentials"
                };
            }

            return new AuthResponseDto
            {
                Success = true,
                Message = "Login successful",
                Name = user.Name,
                Email = user.Email,
                Token = "sample-jwt-token"
            };
        }

        public AuthResponseDto GoogleLogin(GoogleLoginDto dto)
        {
            var user = _repository.GetUserByEmail(dto.Email);

            if (user == null)
            {
                user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    PasswordHash = "GoogleAuth",
                    IsGoogleUser = true
                };

                _repository.AddUser(user);
            }

            return new AuthResponseDto
            {
                Success = true,
                Message = "Google login successful",
                Name = user.Name,
                Email = user.Email,
                Token = "sample-google-jwt-token"
            };
        }

        public AuthResponseDto GetCurrentUser(string email)
        {
            var user = _repository.GetUserByEmail(email);

            if (user == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            return new AuthResponseDto
            {
                Success = true,
                Message = "User fetched successfully",
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}