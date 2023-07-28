using Application.Dto;
using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public Task<IActionResult> Test()
    {
        var response = new { hello = "Hello" };
        return Task.FromResult<IActionResult>(Ok(response));
    }

    //// TODO Only for development
    //[HttpGet]
    //[Authorize(Roles = "ADMIN")]
    //public async Task<IActionResult> GetAll()
    //{
    //    var users = await _userService.GetAll();
    //    return Ok(users);
    //}

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
            _logger.Log(LogLevel.Information, $"Login user: {loginUserDto.Email}");
            LoginResponse loginResponse = await _userService.Login(loginUserDto);
            return Ok(loginResponse);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to login user: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUser([FromBody] int userId)
    {
        _logger.Log(LogLevel.Information, "Delete user");
        try
        {
            User deletedUser = await _userService.DeleteUser(userId);
            _logger.Log(LogLevel.Information, $"Deleted user with id: {deletedUser.Id}");
            return Ok(deletedUser);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to delete user: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

}
