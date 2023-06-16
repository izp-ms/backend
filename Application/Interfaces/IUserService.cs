using Application.Dto;
using Domain.Enums;

namespace Application.Interfaces;

public interface IUserService
{
    Task<RegistrationResult> Register(RegisterUserDto registerUserDto);
    Task<string> Login(LoginUserDto loginUserDto);
}
