using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserDetailController : ControllerBase
{
    private readonly IUserDetailService _userDetailService;
    private readonly IUserContextService _userContextService;
    private readonly ILogger<UserDetailController> _logger;

    public UserDetailController(IUserDetailService userDetailService, IUserContextService userContextService, ILogger<UserDetailController> logger)
    {
        _userDetailService = userDetailService;
        _userContextService = userContextService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserDetail()
    {
        _logger.Log(LogLevel.Information, "Get user detail");
        try
        {
            UserDetailDto userDetailDto = await _userDetailService.GetUserDetailById((int)_userContextService.GetUserId);
            return Ok(userDetailDto);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get user detail: {ex.Message}");
            return BadRequest(new { message = $"Failed to get user detail: {ex.Message}" });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserDetail([FromBody] UserDetailDto userDetail)
    {
        _logger.Log(LogLevel.Information, "Update user detail");
        try
        {
            if (_userContextService.GetUserId != userDetail.Id)
            {
                _logger.Log(LogLevel.Information, $"User with id: {_userContextService.GetUserId} tried to update user detail with id: {userDetail.Id}");
                return BadRequest(new { message = "Unauthorized" });
            }
            UserDetailDto updatedUserDetailDto = await _userDetailService.UpdateUserDetail(userDetail);
            _logger.Log(LogLevel.Information, $"Updated user detail with id: {updatedUserDetailDto.Id}");
            return Ok(updatedUserDetailDto);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to update user detail: {ex.Message}");
            return BadRequest(new { message = $"Failed to update user detail: {ex.Message}" });
        }
    }
}
