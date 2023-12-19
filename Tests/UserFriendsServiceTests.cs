using Application.Dto;
using Application.Interfaces;
using Application.Mappings.Manual;
using Application.Requests;
using Application.Response;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace UserFriendsServiceTests;

public class UserFriendsServiceTests
{
    private readonly Mock<IUserContextService> _userContextServiceMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IUserFriendsRepository> _userFriendsRepositoryMock;
    private readonly IUserFriendsService _userFriendsService;
    private readonly Mock<IMapper> _mapperMock;

    public UserFriendsServiceTests()
    {
        _userContextServiceMock = new Mock<IUserContextService>();
        _userServiceMock = new Mock<IUserService>();
        _userFriendsRepositoryMock = new Mock<IUserFriendsRepository>();
        _mapperMock = new Mock<IMapper>();
        _userFriendsService = new UserFriendsService(
            _userFriendsRepositoryMock.Object,
            _userServiceMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task GetFollowing_ValidPaginationRequest_ShouldReturnPaginationResponse()
    {
        // Arrange
        PaginationRequest pagination = new PaginationRequest { PageNumber = 1, PageSize = 10 };
        FiltersUserRequest filters = new FiltersUserRequest { UserId = 1 };
        IEnumerable<UserFriends> userFollowing = new List<UserFriends>
        {
            new UserFriends { UserId = 1, FriendId = 2 },
            new UserFriends { UserId = 1, FriendId = 3 },
            new UserFriends { UserId = 1, FriendId = 4 },
        };

        IEnumerable<FriendDto> mappedFollowing = new List<FriendDto>
        {
            new FriendDto { Id = 2 },
            new FriendDto { Id = 3 },
            new FriendDto { Id = 4 },
        };

        _userFriendsRepositoryMock.Setup(repo => repo.InsertRange(userFollowing)).ReturnsAsync(userFollowing);
        _userFriendsRepositoryMock.Setup(repo => repo.GetAllFollowing(FiltersMapper.Map(filters))).ReturnsAsync(userFollowing);
        _userFriendsRepositoryMock.Setup(repo => repo.GetPaginatedFollowing(PaginationMapper.Map(pagination), FiltersMapper.Map(filters))).ReturnsAsync(userFollowing);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<FriendDto>>(userFollowing)).Returns(mappedFollowing);

        // Act
        PaginationResponse<FriendDto> result = await _userFriendsService.GetPaginatedFollowing(pagination, filters);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginationResponse<FriendDto>>(result);
    }


    [Fact]
    public async Task IsFollowing_ValidUserIdAndFriendId_ShouldReturnTrue()
    {
        // Arrange
        int validUserId = 1;
        int validFriendId = 2;
        UserFriends userFriendsEntity = new UserFriends { UserId = validUserId, FriendId = validFriendId };

        _userFriendsRepositoryMock.Setup(repo => repo.GetByUserIdAndFriendId(validUserId, validFriendId)).ReturnsAsync(userFriendsEntity);

        // Act
        bool result = await _userFriendsService.IsFollowing(validUserId, validFriendId);

        // Assert
        Assert.True(result);
    }

}