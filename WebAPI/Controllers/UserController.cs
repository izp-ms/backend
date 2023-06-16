using Application.Dto;
using Application.Interfaces;
using Domain.Enums;
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

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
    {
        _logger.Log(LogLevel.Information, "Register user");
        RegistrationResult registeredUser = await _userService.Register(registerUserDto);
        _logger.Log(LogLevel.Information, $"Created new user: {registerUserDto.NickName}");

        return Created("api/register", registeredUser.ToString());
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        string jwtToken = await _userService.Login(loginUserDto);
        return Ok(jwtToken);
    }
}
