using Application.Dto;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<RegisterResponse> Register(RegisterUserDto registerUserDto);
    Task<LoginResponse> Login(LoginUserDto loginUserDto);
    Task<User> DeleteUser(int userId);
    Task<UserDto> GetUser(int userId);
    Task<UserUpdateDto> UpdateUser(UserUpdateDto userUpdateDto);
}
