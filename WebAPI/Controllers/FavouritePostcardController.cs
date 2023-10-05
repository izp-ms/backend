using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FavouritePostcardController : ControllerBase
{
    private readonly IFavouritePostcardService _favouritePostcardService;
    private readonly IUserContextService _userContextService;
    private readonly ILogger<FavouritePostcardController> _logger;

    public FavouritePostcardController(IFavouritePostcardService favouritePostcardService, IUserContextService userContextService, ILogger<FavouritePostcardController> logger)
    {
        _favouritePostcardService = favouritePostcardService;
        _userContextService = userContextService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetFavouritePostcardsByUserId([FromQuery] int userId)
    {
        _logger.Log(LogLevel.Information, "Get favourite postcards");
        try
        {
            IEnumerable<FavouritePostcardDto> favouritePostcardDtos = await _favouritePostcardService.GetFavouritePostcards(userId);
            return Ok(favouritePostcardDtos);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get favourite postcards: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFavouritePostcards([FromBody] UpdateFavouritePostcardRequest updateFavouritePostcardRequest)
    {
        _logger.Log(LogLevel.Information, "Update favourite postcards");
        try
        {
            if (_userContextService.GetUserId != updateFavouritePostcardRequest.UserId)
            {
                _logger.Log(LogLevel.Information, $"User with id: {_userContextService.GetUserId} tried to update favourite postcards with id: {updateFavouritePostcardRequest.UserId}");
                return BadRequest(new { message = "Unauthorized" });
            }
            IEnumerable<FavouritePostcardDto> updatedFavouritePostcards = await _favouritePostcardService.UpdateFavouritePostcards(updateFavouritePostcardRequest);
            _logger.Log(LogLevel.Information, $"Updated favourite postcards with id: {updatedFavouritePostcards}");
            return Ok(updatedFavouritePostcards);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to update favourite postcards: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }
}
