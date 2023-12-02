using Application.Dto;
using Application.Helpers;
using Application.Interfaces;
using Application.Requests;
using Application.Response;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserFriendsController : ControllerBase
{
    private readonly IUserFriendsService _userFriendsService;
    private readonly IUserContextService _userContextService;
    private readonly IMemoryCache _cache;
    private readonly CacheSettings _cacheSettings;
    private readonly ILogger<UserFriendsController> _logger;

    public UserFriendsController(
        IUserFriendsService userFriendsService,
        IUserContextService userContextService,
        IMemoryCache cache,
        CacheSettings cacheSettings,
        ILogger<UserFriendsController> logger
    )
    {
        _userFriendsService = userFriendsService;
        _userContextService = userContextService;
        _cache = cache;
        _cacheSettings = cacheSettings;
        _logger = logger;
    }

    [HttpGet("Following")]
    public async Task<IActionResult> GetPaginatedFollowings(
        [FromQuery] PaginationRequest pagination,
        [FromQuery] FiltersUserRequest filters
    )
    {
        _logger.Log(LogLevel.Information, "Get following");
        string cacheKey = CacheKeyGenerator.GetKey(_userContextService.GetUserId, pagination, filters);

        try
        {
            if (!_cache.TryGetValue(cacheKey, out PaginationResponse<FriendDto> followings))
            {
                followings = await _userFriendsService.GetPaginatedFollowing(pagination, filters);
                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(_cacheSettings.CacheTimeInSeconds))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(_cacheSettings.CacheTimeInSeconds))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cacheKey, followings, cacheEntryOptions);
            }

            return Ok(followings);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get following: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("Followers")]
    public async Task<IActionResult> GetPaginatedFollowers(
        [FromQuery] PaginationRequest pagination,
        [FromQuery] FiltersUserRequest filters)
    {
        _logger.Log(LogLevel.Information, "Get followers");
        string cacheKey = CacheKeyGenerator.GetKey(_userContextService.GetUserId, pagination, filters);

        try
        {
            if (!_cache.TryGetValue(cacheKey, out PaginationResponse<FriendDto> followers))
            {
                followers = await _userFriendsService.GetPaginatedFollowers(pagination, filters);
                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(_cacheSettings.CacheTimeInSeconds))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(_cacheSettings.CacheTimeInSeconds))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cacheKey, followers, cacheEntryOptions);
            }

            return Ok(followers);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get followers: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("IsFollowing/{id}")]
    public async Task<IActionResult> IsFollowing(int id)
    {
        _logger.Log(LogLevel.Information, "Check if user is following");
        try
        {
            bool isFollowing = await _userFriendsService.IsFollowing((int)_userContextService.GetUserId, id);
            return Ok(isFollowing);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to check if user is following: {ex.Message}");
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
