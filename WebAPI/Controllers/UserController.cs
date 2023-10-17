using Application.Dto;
using Application.Helpers;
using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserContextService _userContextService;
    private readonly IMemoryCache _cache;
    private readonly ILogger<UserController> _logger;

    public UserController(
        IUserService userService,
        IUserContextService userContextService,
        IMemoryCache cache,
        ILogger<UserController> logger)
    {
        _userService = userService;
        _userContextService = userContextService;
        _cache = cache;
        _logger = logger;
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Test");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUser([FromQuery] int userId)
    {
        _logger.Log(LogLevel.Information, "Get user information");
        string cacheKey = CacheKeyGenerator.GetKey(userId, _userContextService.GetUserId);

        try
        {
            if (!_cache.TryGetValue(cacheKey, out UserDto userData))
            {
                userData = await _userService.GetUser(userId);
                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cacheKey, userData, cacheEntryOptions);
            }

            return Ok(userData);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get user information: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
    {
        _logger.Log(LogLevel.Information, "Update user information");
        try
        {
            if (_userContextService.GetUserId != userUpdateDto.Id)
            {
                _logger.Log(LogLevel.Information, $"User with id: {_userContextService.GetUserId} tried to update user with id: {userUpdateDto.Id}");
                return BadRequest(new { message = "Unauthorized" });
            }
            userUpdateDto.Id = (int)_userContextService.GetUserId;
            UserUpdateDto updatedUser = await _userService.UpdateUser(userUpdateDto);
            _logger.Log(LogLevel.Information, $"Updated user with id: {updatedUser.Id}");

            string cacheKey = CacheKeyGenerator.GetKey(_userContextService.GetUserId, userUpdateDto);
            _cache.Remove(cacheKey);

            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to update user information: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
    {
        _logger.Log(LogLevel.Information, "Register user");
        try
        {
            RegisterResponse registeredUser = await _userService.Register(registerUserDto);
            _logger.Log(LogLevel.Information, $"Created new user: {registerUserDto.NickName}");
            return Created("api/register", registeredUser);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to create new user: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        _logger.Log(LogLevel.Information, "Login user");
        try
        {
            LoginResponse loginResponse = await _userService.Login(loginUserDto);
            return Ok(loginResponse);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Login failed: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUser([FromQuery] int userId)
    {
        _logger.Log(LogLevel.Information, "Delete user");
        try
        {
            if (_userContextService.GetUserId != userId)
            {
                _logger.Log(LogLevel.Information, $"User with id: {_userContextService.GetUserId} tried to delete user with id: {userId}");
                return BadRequest(new { message = "Unauthorized" });
            }
            User deletedUser = await _userService.DeleteUser(userId);
            _logger.Log(LogLevel.Information, $"Deleted user with id: {deletedUser.Id}");
            return Ok(new { message = $"Deleted user with id: {deletedUser.Id}" });
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to delete user: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

}
