using Application.Dto;
using Application.Interfaces;
using Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostcardController : ControllerBase
{
    private readonly IPostcardService _postcardService;
    private readonly IUserContextService _userContextService;
    private readonly ILogger<PostcardController> _logger;

    public PostcardController(IPostcardService postcardService, IUserContextService userContextService, ILogger<PostcardController> logger)
    {
        _postcardService = postcardService;
        _userContextService = userContextService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedPostcards([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        _logger.Log(LogLevel.Information, "Get postcards");
        try
        {
            PaginationRequest paginationRequest = new PaginationRequest() { PageNumber = pageNumber, PageSize = pageSize };
            PaginationResponse<PostcardDto> postcards = await _postcardService.GetPagination(paginationRequest);
            return Ok(postcards);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get postcards: {ex.Message}");
            return BadRequest(new { message = $"Failed to get postcards: {ex.Message}" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostcard([FromRoute] int id)
    {
        _logger.Log(LogLevel.Information, "Get postcard");
        try
        {
            PostcardDto postcard = await _postcardService.GetPostcardById(id);
            return Ok(postcard);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get postcard: {ex.Message}");
            return BadRequest(new { message = $"Failed to get postcard: {ex.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPostcard([FromBody] PostcardDto postcardDto)
    {
        _logger.Log(LogLevel.Information, "Add postcard");
        try
        {
            PostcardDto newPostcard = await _postcardService.AddNewPostcard(postcardDto);
            _logger.Log(LogLevel.Information, $"Added postcard with id: {newPostcard.Id}");
            return Ok(newPostcard);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to add postcard: {ex.Message}");
            return BadRequest(new { message = $"Failed to add postcard: {ex.Message}" });
        }
    }

    [HttpPut("Transfer")]
    public async Task<IActionResult> TransferPostcard([FromBody] TransferPostcardRequest transferPostcardRequest)
    {
        _logger.Log(LogLevel.Information, "Transfer postcard");
        try
        {
            UserPostcardDto userPostcard = await _postcardService.TransferPostcard(transferPostcardRequest.PostcardId, transferPostcardRequest.NewUserId);
            _logger.Log(LogLevel.Information, $"Transferred postcard with id: {userPostcard.PostcardId} to user with id: {userPostcard.UserId}");
            return Ok(userPostcard);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to transfer postcard: {ex.Message}");
            return BadRequest(new { message = $"Failed to transfer postcard: {ex.Message}" });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePostcard([FromBody] PostcardDto postcardDto)
    {
        _logger.Log(LogLevel.Information, "Update postcard");
        try
        {
            PostcardDto updatedPostcard = await _postcardService.UpdatePostcard(postcardDto);
            _logger.Log(LogLevel.Information, $"Updated postcard with id: {updatedPostcard.Id}");
            return Ok(updatedPostcard);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to update postcard: {ex.Message}");
            return BadRequest(new { message = $"Failed to update postcard: {ex.Message}" });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePostcard([FromQuery] int postcardId)
    {
        _logger.Log(LogLevel.Information, "Delete postcard");
        try
        {
            await _postcardService.DeletePostcard(postcardId);
            _logger.Log(LogLevel.Information, $"Deleted postcard with id: {postcardId}");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to delete postcard: {ex.Message}");
            return BadRequest(new { message = $"Failed to delete postcard: {ex.Message}" });
        }
    }
}
