using Application.Dto;

namespace Application.Interfaces;

public interface IUserStatsService
{
    Task<UserStatDto> GetUserStatsById(int userId);
    Task<UserStatDto> UpdateUserStats(UserStatDto userStatsDto);
}
