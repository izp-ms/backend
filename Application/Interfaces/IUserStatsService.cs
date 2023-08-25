using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserStatsService
{
    Task<UserStat> AddUserStats(UserStatDto userStatsDto);
    Task<UserStat> GetUserStatsById(int userId);
    Task<UserStat> UpdateUserStats(UserStatDto userStatsDto);
    Task<UserStat> DeleteUserStatsById(int userId);
}
