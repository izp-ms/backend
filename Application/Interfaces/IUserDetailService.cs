using Application.Dto;

namespace Application.Interfaces;

public interface IUserDetailService
{
    Task<UserDetailDto> GetUserDetailById(int userId);
    Task<UserDetailDto> UpdateUserDetail(UserDetailDto userDetailDto);
}
