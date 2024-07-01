using Identity.Domain.Models;
using Identity.Domain.Models.Dto;

namespace Identity.Domain.Repository;

public interface IAuthRepository
{
    Task<string> Register(RegisterModel register);

    Task<LoginResponseDto> Login(LoginModel login);

    Task<ResponseDto> ProfileService(string id);

    Task<ResponseDto> UpdateProfile(string id, UpdateProfile updateProfiles);

    // Task<string> SendActivateEmail(string userId);
}