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

    public async Task<UserStatDto> GetUserStatsById(int userId)
    {
        UserStat userStat = await _userStatsRepository.Get(userId) ?? throw new Exception(userId.ToString());
        return _mapper.Map<UserStatDto>(userStat);
    }

    public async Task<UserStatDto> UpdateUserStats(UserStatDto userStatsDto)
    {
        UserStat mappedUserStats = _mapper.Map<UserStat>(userStatsDto);
        if (mappedUserStats == null)
        {
            throw new Exception($"UserStats with id {userStatsDto.Id} not found");
        }
        UserStat userStat = await _userStatsRepository.Update(mappedUserStats) ?? throw new Exception("UserStats not updated");
        return _mapper.Map<UserStatDto>(userStat);
    }
}
