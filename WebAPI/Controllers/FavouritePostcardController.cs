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

}
