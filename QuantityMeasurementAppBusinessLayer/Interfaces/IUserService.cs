using QuantityMeasurementAppModel.DTOs;

namespace QuantityMeasurementAppBusinessLayer.Interfaces
{
    public interface IUserService
    {
        AuthResponseDto Register(RegisterDto dto);
        AuthResponseDto Login(LoginDto dto);
        AuthResponseDto GoogleLogin(GoogleLoginDto dto);
        AuthResponseDto GetCurrentUser(string email);
    }
}