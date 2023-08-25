﻿using Application.Dto;
using Application.Interfaces;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostcardImageController : ControllerBase
{
    private readonly IPostcardImageService _postcardImageService;
    private readonly IUserContextService _userContextService;
    private readonly ILogger<PostcardImageController> _logger;

    public PostcardImageController(IPostcardImageService postcardImageService, IUserContextService userContextService, ILogger<PostcardImageController> logger)
    {
        _postcardImageService = postcardImageService;
        _userContextService = userContextService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedPostcardImages([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        _logger.Log(LogLevel.Information, "Get postcard images");
        try
        {
            PaginationRequest paginationRequest = new PaginationRequest() { PageNumber = pageNumber, PageSize = pageSize };
            PaginationResponse<PostcardImageDto> postcardImages = await _postcardImageService.GetPagination(paginationRequest);
            return Ok(postcardImages);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get postcard images: {ex.Message}");
            return BadRequest(new { message = $"Failed to get postcard images: {ex.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPostcardImage([FromBody] PostcardImageDto postcardImageDto)
    {
        _logger.Log(LogLevel.Information, "Add postcard image");
        try
        {
            PostcardImageDto newPostcardImage = await _postcardImageService.AddNewPostcardImage(postcardImageDto);
            _logger.Log(LogLevel.Information, $"Added postcard image with id: {newPostcardImage.Id}");
            return Ok(newPostcardImage);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to add postcard image: {ex.Message}");
            return BadRequest(new { message = $"Failed to add postcard image: {ex.Message}" });
        }
    }
}
