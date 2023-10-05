using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;
    private readonly IUserContextService _userContextService;
    private readonly ILogger<AddressController> _logger;

    public AddressController(IAddressService addressService, IUserContextService userContextService, ILogger<AddressController> logger)
    {
        _addressService = addressService;
        _userContextService = userContextService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAddress()
    {
        _logger.Log(LogLevel.Information, "Get address");
        try
        {
            AddressDto addressDto = await _addressService.GetAddress((int)_userContextService.GetUserId);
            return Ok(addressDto);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get address: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAddress([FromBody] AddressDto addressDto)
    {
        _logger.Log(LogLevel.Information, "Update user stats");
        try
        {
            if (_userContextService.GetUserId != addressDto.Id)
            {
                _logger.Log(LogLevel.Information, $"User with id: {_userContextService.GetUserId} tried to update address with id: {addressDto.Id}");
                return BadRequest(new { message = "Unauthorized" });
            }
            AddressDto updatedAddress = await _addressService.UpdateAddress(addressDto);
            _logger.Log(LogLevel.Information, $"Updated address with id: {updatedAddress.Id}");
            return Ok(updatedAddress);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to update address: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }
}
