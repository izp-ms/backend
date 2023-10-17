﻿using Application.Dto;
using Application.Interfaces;
using Application.Requests;
using Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostcardDataController : ControllerBase
{
    private readonly IPostcardDataService _postcardDataService;
    private readonly IUserContextService _userContextService;
    private readonly IMemoryCache _cache;
    private readonly ILogger<PostcardDataController> _logger;

    public PostcardDataController(
        IPostcardDataService postcardDataService,
        IUserContextService userContextService,
        IMemoryCache cache,
        ILogger<PostcardDataController> logger)
    {
        _postcardDataService = postcardDataService;
        _userContextService = userContextService;
        _cache = cache;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedPostcardData([FromQuery] PaginatedPostcardDataRequest request)
    {
        _logger.Log(LogLevel.Information, "Get postcard data");
        string cacheKey = $"postcard-{request.PageNumber}-{request.PageSize}-{request.UserId}-{_userContextService.GetUserId}";

        try
        {
            if (!_cache.TryGetValue(cacheKey, out PaginationResponse<PostcardDataDto> postcardData))
            {
                postcardData = await _postcardDataService.GetPagination(request);
                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cacheKey, postcardData, cacheEntryOptions);
            }

            return Ok(postcardData);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get postcard data: {ex.Message}");
            return BadRequest(new { message = ex.Message });
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
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpPost("NewPostcard")]
    public async Task<IActionResult> GetNewPostcard([FromBody] CoordinateRequest coordinateRequest)
    {
        _logger.Log(LogLevel.Information, "Get new postcard data");
        string cacheKey = $"postcard-{coordinateRequest.Latitude}-{coordinateRequest.Longitude}-{_userContextService.GetUserId}";
        try
        {
            if (!_cache.TryGetValue(cacheKey, out CurrentLocationPostcardsResponse postcardData))
            {
                postcardData = await _postcardDataService.GetPostcardsNearby(coordinateRequest);
                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cacheKey, postcardData, cacheEntryOptions);
            }

            return Ok(postcardData);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get new postcard data: {ex.Message}");
            return BadRequest(new { message = ex.Message });
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
            return BadRequest(new { message = ex.Message });
        }
    }
}
