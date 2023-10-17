using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostcardCollectionController : ControllerBase
{
    private readonly IPostcardCollectionService _postcardCollectionService;
    private readonly ILogger<PostcardCollectionController> _logger;

    public PostcardCollectionController(IPostcardCollectionService postcardCollectionService, ILogger<PostcardCollectionController> logger)
    {
        _postcardCollectionService = postcardCollectionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetPostcardCollection([FromQuery] int userId)
    {
        _logger.Log(LogLevel.Information, "Get postcard collection");
        try
        {
            PostcardCollectionDto postcardCollectionDto = await _postcardCollectionService.GetPostcardCollection(userId);
            return Ok(postcardCollectionDto);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get postcard collection: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }
}
