using Application.Dto;
using Application.Interfaces;
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

    public UserStatsController(IUserStatsService userStatsService, IUserContextService userContextService, ILogger<UserStatsController> logger)
    {
        _userStatsService = userStatsService;
        _userContextService = userContextService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserStats()
    {
        _logger.Log(LogLevel.Information, "Get user stats");
        try
        {
            UserStatDto userStatDto = await _userStatsService.GetUserStatsById((int)_userContextService.GetUserId);
            return Ok(userStatDto);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get user stats: {ex.Message}");
            return BadRequest(new { message = ex.Message });
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
            UserStatDto updatedUserStatDto = await _userStatsService.UpdateUserStats(userStats);
            _logger.Log(LogLevel.Information, $"Updated user stats with id: {updatedUserStatDto.Id}");
            return Ok(updatedUserStatDto);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to update user stats: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }
}
