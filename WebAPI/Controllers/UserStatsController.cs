using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserStatsController : ControllerBase
{
    private readonly IUserStatsService _userStatsService;
    private readonly IUserContextService _userContextService;
    private readonly ILogger<UserStatsController> _logger;
    private readonly IMapper _mapper;

    public UserStatsController(IUserStatsService userStatsService, IUserContextService userContextService, IMapper mapper, ILogger<UserStatsController> logger)
    {
        _userStatsService = userStatsService;
        _userContextService = userContextService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserStats()
    {
        _logger.Log(LogLevel.Information, "Get user stats");
        try
        {
            UserStat userStats = await _userStatsService.GetUserStatsById((int)_userContextService.GetUserId);
            return Ok(_mapper.Map<UserStatDto>(userStats));
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get user stats: {ex.Message}");
            return BadRequest(new { message = $"Failed to get user stats: {ex.Message}" });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserStats([FromBody] UserStatDto userStats)
    {
        _logger.Log(LogLevel.Information, "Update user stats");
        try
        {
            if (_userContextService.GetUserId != userStats.Id)
            {
                _logger.Log(LogLevel.Information, $"User with id: {_userContextService.GetUserId} tried to update user stats with id: {userStats.Id}");
                return BadRequest(new { message = "Unauthorized" });
            }
            UserStat updatedUserStats = await _userStatsService.UpdateUserStats(userStats);
            _logger.Log(LogLevel.Information, $"Updated user stats with id: {updatedUserStats.Id}");
            return Ok(_mapper.Map<UserStatDto>(updatedUserStats));
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to update user stats: {ex.Message}");
            return BadRequest(new { message = $"Failed to update user stats: {ex.Message}" });
        }
    }
}
