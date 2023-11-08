using Application.Dto;
using Application.Requests;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<RegisterResponse> Register(RegisterUserDto registerUserDto);
    Task<LoginResponse> Login(LoginUserDto loginUserDto);
    Task<User> DeactivateUser(int userId);
    Task<UserDto> GetUser(int userId);
    Task<PaginationResponse<UserDto>> GetPagination(PaginationRequest pagination, FiltersUserRequest filters);
    Task<UserUpdateDto> UpdateUser(UserUpdateDto userUpdateDto);
    Task<bool> IsUserActive(int userId);
}
