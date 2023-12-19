using Application.Dto;
using Application.Interfaces;
using Application.Requests;
using Application.Response;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Models;
using Moq;
using Xunit;

namespace PostcardServiceTests;

public class PostcardServiceTests
{
    private readonly Mock<IPostcardRepository> _postcardRepositoryMock;
    private readonly Mock<IUserPostcardRepository> _userPostcardRepositoryMock;
    private readonly Mock<IUserContextService> _userContextServiceMock;
    private readonly Mock<IUserStatsService> _userStatsServiceMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly PostcardService _postcardService;

    public PostcardServiceTests()
    {
        _postcardRepositoryMock = new Mock<IPostcardRepository>();
        _userPostcardRepositoryMock = new Mock<IUserPostcardRepository>();
        _userContextServiceMock = new Mock<IUserContextService>();
        _userStatsServiceMock = new Mock<IUserStatsService>();
        _userServiceMock = new Mock<IUserService>();
        _mapperMock = new Mock<IMapper>();
        _postcardService = new PostcardService(
            _userPostcardRepositoryMock.Object,
            _postcardRepositoryMock.Object,
            _userContextServiceMock.Object,
            _userStatsServiceMock.Object,
            _userServiceMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task AddNewPostcard_ValidPostcardDto_ShouldReturnMappedPostcardDto()
    {
        // Arrange
        PostcardDto postcardDto = new PostcardDto
        {
            Title = "Test Postcard",
            Content = "This is a test postcard",
            PostcardDataId = 1,
            UserId = 1,
            IsSent = false
        };

        Postcard postcardEntity = new Postcard();
        UserPostcard newUserPostcard = new UserPostcard();

        _mapperMock.Setup(mapper => mapper.Map<Postcard>(postcardDto)).Returns(postcardEntity);
        _postcardRepositoryMock.Setup(repo => repo.Insert(postcardEntity)).ReturnsAsync(new Postcard());
        _userPostcardRepositoryMock.Setup(repo => repo.Insert(It.IsAny<UserPostcard>())).ReturnsAsync(newUserPostcard);
        _mapperMock.Setup(mapper => mapper.Map<PostcardDto>(It.IsAny<Postcard>())).Returns(postcardDto);

        // Act
        PostcardDto result = await _postcardService.AddNewPostcard(postcardDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(postcardDto.UserId, result.UserId);
        Assert.Equal(postcardDto.Title, result.Title);
        Assert.Equal(postcardDto.Content, result.Content);
    }

    [Fact]
    public async Task AddNewPostcard_InvalidPostcardDto_ShouldThrowException()
    {
        // Arrange
        PostcardDto? invalidPostcardDto = null;

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _postcardService.AddNewPostcard(invalidPostcardDto));
    }

    [Fact]
    public async Task GetPostcardById_InvalidPostcardId_ShouldThrowException()
    {
        // Arrange
        int invalidPostcardId = -1;

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _postcardService.GetPostcardById(invalidPostcardId));
    }

    [Fact]
    public async Task GetPostcardById_ValidPostcardId_ShouldReturnMappedPostcardDto()
    {
        // Arrange
        int validPostcardId = 1;
        Postcard postcardEntity = new Postcard { };
        PostcardDto postcardDto = new PostcardDto { };

        _postcardRepositoryMock.Setup(repo => repo.Get(validPostcardId)).ReturnsAsync(postcardEntity);
        _mapperMock.Setup(mapper => mapper.Map<PostcardDto>(postcardEntity)).Returns(postcardDto);

        // Act
        PostcardDto result = await _postcardService.GetPostcardById(validPostcardId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(postcardDto.Id, result.Id);
    }

    [Fact]
    public async Task TransferPostcard_InactiveUser_ShouldThrowException()
    {
        // Arrange
        int inactiveUserId = 2;
        var postcardDto = new PostcardDto { UserId = 1 };

        _userServiceMock.Setup(service => service.IsUserActive(inactiveUserId)).ReturnsAsync(false);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _postcardService.TransferPostcard(inactiveUserId, postcardDto));
    }

    [Fact]
    public async Task GetPagination_ValidPaginationAndFilters_ShouldReturnPaginationResponse()
    {
        // Arrange
        PaginationRequest pagination = new PaginationRequest { PageNumber = 1, PageSize = 10 };
        FiltersPostcardRequest filters = new FiltersPostcardRequest { UserId = 1 };

        List<Postcard> allPostcards = new List<Postcard> { };
        List<Postcard> postcards = new List<Postcard> { };

        _userContextServiceMock.Setup(service => service.GetUserId).Returns(1);
        _postcardRepositoryMock.Setup(repo => repo.GetAllPostcardsByUserId(It.IsAny<FiltersPostcard>())).ReturnsAsync(allPostcards);
        _postcardRepositoryMock.Setup(repo => repo.GetPaginationByUserId(It.IsAny<Pagination>(), It.IsAny<FiltersPostcard>())).ReturnsAsync(postcards);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<PostcardWithDataDto>>(It.IsAny<IEnumerable<Postcard>>())).Returns(new List<PostcardWithDataDto>());

        // Act
        PaginationResponse<PostcardWithDataDto> result = await _postcardService.GetPagination(pagination, filters);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginationResponse<PostcardWithDataDto>>(result);
    }

}
