using Application.Dto;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<RegisterResponse> Register(RegisterUserDto registerUserDto);
    Task<LoginResponse> Login(LoginUserDto loginUserDto);
    Task<User> DeleteUser(int userId);

    Task<IEnumerable<User>> GetAll();
}
