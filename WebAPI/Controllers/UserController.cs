﻿using Application.Dto;
using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserContextService _userContextService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, IUserContextService userContextService, ILogger<UserController> logger)
    {
        _userService = userService;
        _userContextService = userContextService;
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
            return BadRequest(new { message = $"Failed to delete user: {ex.Message}" });
        }
    }

}
