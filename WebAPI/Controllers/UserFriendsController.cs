using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserFriendsController : ControllerBase
{
    private readonly IUserFriendsService _userFriendsService;
    private readonly IUserContextService _userContextService;
    private readonly ILogger<UserFriendsController> _logger;

    public UserFriendsController(IUserFriendsService userFriendsService, IUserContextService userContextService, ILogger<UserFriendsController> logger)
    {
        _userFriendsService = userFriendsService;
        _userContextService = userContextService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFriendsByUserId(int id)
    {
        _logger.Log(LogLevel.Information, "Get friends");
        try
        {
            IEnumerable<FriendDto> friends = await _userFriendsService.GetFriends(id);
            return Ok(friends);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get friends: {ex.Message}");
            return BadRequest(new { message = $"Failed to get friends: {ex.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddNewFriend([FromBody] UserFriendRequest addUserFriendRequest)
    {
        _logger.Log(LogLevel.Information, "Update user stats");
        try
        {
            if (_userContextService.GetUserId != addUserFriendRequest.UserId)
            {
                _logger.Log(LogLevel.Information, $"User with id: {_userContextService.GetUserId} tried to add new friend");
                return BadRequest(new { message = "Unauthorized" });
            }
            FriendDto friendDto = await _userFriendsService.AddNewFriend(addUserFriendRequest);
            _logger.Log(LogLevel.Information, $"Successfully added new friend with id: {friendDto.Id}");
            return Ok(friendDto);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to update friends: {ex.Message}");
            return BadRequest(new { message = $"Failed to update friends: {ex.Message}" });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFriend([FromBody] UserFriendRequest deleteUserFriendRequest)
    {
        _logger.Log(LogLevel.Information, "Update user stats");
        try
        {
            if (_userContextService.GetUserId != deleteUserFriendRequest.UserId)
            {
                _logger.Log(LogLevel.Information, $"User with id: {_userContextService.GetUserId} tried to delete friend");
                return BadRequest(new { message = "Unauthorized" });
            }
            FriendDto friendDto = await _userFriendsService.DeleteFriend(deleteUserFriendRequest);
            _logger.Log(LogLevel.Information, $"Successfully deleted friend with id: {friendDto.Id}");
            return Ok(friendDto);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to update friends: {ex.Message}");
            return BadRequest(new { message = $"Failed to update friends: {ex.Message}" });
        }
    }
}
