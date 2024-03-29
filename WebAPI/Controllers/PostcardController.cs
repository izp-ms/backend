﻿using Application.Dto;
using Application.Helpers;
using Application.Interfaces;
using Application.Requests;
using Application.Response;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostcardController : ControllerBase
{
    private readonly IPostcardService _postcardService;
    private readonly IUserContextService _userContextService;
    private readonly IMemoryCache _cache;
    private readonly CacheSettings _cacheSettings;
    private readonly ILogger<PostcardController> _logger;

    public PostcardController(
        IPostcardService postcardService,
        IUserContextService userContextService,
        IMemoryCache cache,
        CacheSettings cacheSettings,
        ILogger<PostcardController> logger)
    {
        _postcardService = postcardService;
        _userContextService = userContextService;
        _cache = cache;
        _cacheSettings = cacheSettings;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedPostcardsByUserId(
        [FromQuery] PaginationRequest pagination,
        [FromQuery] FiltersPostcardRequest filters
    )
    {
        _logger.Log(LogLevel.Information, "Get postcards");
        if (filters.UserId == 0)
        {
            return BadRequest(new { message = "User id is required" });
        }

        string cacheKey = CacheKeyGenerator.GetKey(_userContextService.GetUserId, pagination, filters);

        try
        {
            if (!_cache.TryGetValue(cacheKey, out PaginationResponse<PostcardWithDataDto> postcards))
            {
                postcards = await _postcardService.GetPagination(pagination, filters);
                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(_cacheSettings.CacheTimeInSeconds))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(_cacheSettings.CacheTimeInSeconds))
                    .SetPriority(CacheItemPriority.Normal);
                _cache.Set(cacheKey, postcards, cacheEntryOptions);
            }

            postcards = await _postcardService.GetPagination(pagination, filters);

            return Ok(postcards);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to get postcards: {ex.Message}");
            return BadRequest(new { message = ex.Message });
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
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("Transfer")]
    public async Task<IActionResult> TransferPostcard([FromBody] TransferPostcardRequest transferPostcardRequest)
    {
        _logger.Log(LogLevel.Information, "Transfer postcard");
        try
        {
            UserPostcardDto userPostcard = await _postcardService.TransferPostcard(transferPostcardRequest.NewUserId, transferPostcardRequest.PostcardDto);
            _logger.Log(LogLevel.Information, $"Transferred postcard with id: {userPostcard.PostcardId} to user with id: {userPostcard.UserId}");
            return Ok(userPostcard);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Information, $"Failed to transfer postcard: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }
}
