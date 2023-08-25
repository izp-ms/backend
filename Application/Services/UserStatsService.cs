using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class UserStatsService : IUserStatsService
{
    private readonly IUserStatsRepository _userStatsRepository;
    private readonly IMapper _mapper;

    public UserStatsService(IUserStatsRepository userStatsRepository, IMapper mapper)
    {
        _userStatsRepository = userStatsRepository;
        _mapper = mapper;
    }

    public async Task<UserStat> GetUserStatsById(int userId)
    {
        return await _userStatsRepository.Get(userId) ?? throw new Exception(userId.ToString());
    }

    public async Task<UserStat> AddUserStats(UserStatDto userStatsDto)
    {
        UserStat mappedUserStats = _mapper.Map<UserStat>(userStatsDto);
        return await _userStatsRepository.Insert(mappedUserStats) ?? throw new Exception("UserStats not created");
    }

    public async Task<UserStat> UpdateUserStats(UserStatDto userStatsDto)
    {
        UserStat mappedUserStats = _mapper.Map<UserStat>(userStatsDto);
        if (mappedUserStats == null)
        {
            throw new Exception($"UserStats with id {userStatsDto.Id} not found");
        }
        return await _userStatsRepository.Update(mappedUserStats) ?? throw new Exception("UserStats not updated");
    }

    public async Task<UserStat> DeleteUserStatsById(int statsId)
    {
        UserStat userStatsToDelete = await _userStatsRepository.Get(statsId) ?? throw new Exception(statsId.ToString());
        return await _userStatsRepository.Delete(userStatsToDelete);
    }
}
