using Application.Dto;
using Application.Interfaces;
using Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostcardDataController : ControllerBase
{
    private readonly IPostcardDataService _postcardDataService;
    private readonly ILogger<PostcardDataController> _logger;

    public PostcardDataController(IPostcardDataService postcardDataService, IUserContextService userContextService, ILogger<PostcardDataController> logger)
    {
        _postcardDataService = postcardDataService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedPostcardData([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        _logger.Log(LogLevel.Information, "Get postcard data");
        try
        {
            PaginationRequest paginationRequest = new PaginationRequest() { PageNumber = pageNumber, PageSize = pageSize };
            PaginationResponse<PostcardDataDto> postcardData = await _postcardDataService.GetPagination(paginationRequest);
            return Ok(postcardData);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get postcard data: {ex.Message}");
            return BadRequest(new { message = $"Failed to get postcard data: {ex.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPostcardData([FromBody] PostcardDataDto postcardDataDto)
    {
        _logger.Log(LogLevel.Information, "Add postcard data");
        try
        {
            PostcardDataDto newPostcardData = await _postcardDataService.AddNewPostcardData(postcardDataDto);
            _logger.Log(LogLevel.Information, $"Added postcard data with id: {newPostcardData.Id}");
            return Ok(newPostcardData);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to add postcard data: {ex.Message}");
            return BadRequest(new { message = $"Failed to add postcard data: {ex.Message}" });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePostcardData([FromQuery] int postcardDataId)
    {
        _logger.Log(LogLevel.Information, "Delete postcard data");
        try
        {
            await _postcardDataService.DeletePostcardData(postcardDataId);
            _logger.Log(LogLevel.Information, $"Deleted postcard data with id: {postcardDataId}");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to delete postcard data: {ex.Message}");
            return BadRequest(new { message = $"Failed to delete postcard data: {ex.Message}" });
        }
    }
}
