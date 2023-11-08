using Application.Dto;
using Application.Interfaces;
using Application.Requests;
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
    public async Task<IActionResult> GetFollowingByUserId(int id)
    {
        _logger.Log(LogLevel.Information, "Get following");
        try
        {
            IEnumerable<FriendDto> following = await _userFriendsService.GetFollowing(id);
            return Ok(following);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get following: {ex.Message}");
            return BadRequest(new { message = ex.Message });
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
            addUserFriendRequest.UserId = (int)_userContextService.GetUserId;
            FriendDto friendDto = await _userFriendsService.AddNewFriend(addUserFriendRequest);
            _logger.Log(LogLevel.Information, $"Successfully added new friend with id: {friendDto.Id}");
            return Ok(friendDto);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to update friends: {ex.Message}");
            return BadRequest(new { message = ex.Message });
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
            return BadRequest(new { message = ex.Message });
        }
    }
}
