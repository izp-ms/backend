namespace UserServiceTests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUserStatsRepository> _userStatsRepositoryMock;
    private readonly Mock<IUserDetailRepository> _userDetailRepositoryMock;
    private readonly Mock<IAddressRepository> _addressRepositoryMock;
    private readonly Mock<IUserFriendsRepository> _userFriendsRepositoryMock;
    private readonly Mock<IPostcardDataRepository> _postcardDataRepositoryMock;
    private readonly Mock<IUserContextService> _userContextServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userStatsRepositoryMock = new Mock<IUserStatsRepository>();
        _userDetailRepositoryMock = new Mock<IUserDetailRepository>();
        _addressRepositoryMock = new Mock<IAddressRepository>();
        _userFriendsRepositoryMock = new Mock<IUserFriendsRepository>();
        _postcardDataRepositoryMock = new Mock<IPostcardDataRepository>();
        _userContextServiceMock = new Mock<IUserContextService>();
        _mapperMock = new Mock<IMapper>();
        _passwordHasherMock = new Mock<IPasswordHasher<User>>();
        _userService = new UserService(
            _userContextServiceMock.Object,
            _userRepositoryMock.Object,
            _userStatsRepositoryMock.Object,
            _userDetailRepositoryMock.Object,
            _addressRepositoryMock.Object,
            _userFriendsRepositoryMock.Object,
            _postcardDataRepositoryMock.Object,
            _mapperMock.Object,
            _passwordHasherMock.Object
        );
    }

    [Fact]
    public async Task Login_ValidCredentials_ShouldReturnToken()
    {
        // Arrange
        LoginUserDto loginUserDto = new LoginUserDto { Email = "test@example.com", Password = "password" };
        User storedUser = new User { Email = "test@example.com", Password = "hashedPassword" };
        _userRepositoryMock.Setup(repo => repo.GetUserByEmail(loginUserDto.Email)).Returns(storedUser);
        _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(storedUser, storedUser.Password, loginUserDto.Password))
            .Returns(PasswordVerificationResult.Success);
        _userRepositoryMock.Setup(repo => repo.Login(storedUser)).Returns("token");

        // Act
        LoginResponse result = await _userService.Login(loginUserDto);

        // Assert
        Assert.Equal("token", result.Token);
    }

    [Fact]
    public async Task GetUser_ValidUserId_ShouldReturnMappedUserDto()
    {
        // Arrange
        int validUserId = 1;
        var userEntity = new User { IsActive = true };
        var addressEntity = new Address { };
        var userDetailEntity = new UserDetail { };
        var userStatEntity = new UserStat { };

        _userRepositoryMock.Setup(repo => repo.Get(validUserId)).ReturnsAsync(userEntity);
        _addressRepositoryMock.Setup(repo => repo.Get(validUserId)).ReturnsAsync(addressEntity);
        _userDetailRepositoryMock.Setup(repo => repo.Get(validUserId)).ReturnsAsync(userDetailEntity);
        _userStatsRepositoryMock.Setup(repo => repo.Get(validUserId)).ReturnsAsync(userStatEntity);
        _postcardDataRepositoryMock.Setup(repo => repo.TotalCountByUserId(validUserId)).ReturnsAsync(10);
        _userFriendsRepositoryMock.Setup(repo => repo.FollowersCount(validUserId)).ReturnsAsync(5);
        _userFriendsRepositoryMock.Setup(repo => repo.FollowingCount(validUserId)).ReturnsAsync(5);
        _mapperMock.Setup(mapper => mapper.Map<UserDto>(It.IsAny<User>())).Returns(new UserDto());

        // Act
        var result = await _userService.GetUser(validUserId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UserDto>(result);
    }

    [Fact]
    public async Task GetUser_InactiveUserId_ShouldThrowException()
    {
        // Arrange
        int validUserId = 1;
        var userEntity = new User { IsActive = false };
        var addressEntity = new Address { };
        var userDetailEntity = new UserDetail { };
        var userStatEntity = new UserStat { };

        _userRepositoryMock.Setup(repo => repo.Get(validUserId)).ReturnsAsync(userEntity);
        _addressRepositoryMock.Setup(repo => repo.Get(validUserId)).ReturnsAsync(addressEntity);
        _userDetailRepositoryMock.Setup(repo => repo.Get(validUserId)).ReturnsAsync(userDetailEntity);
        _userStatsRepositoryMock.Setup(repo => repo.Get(validUserId)).ReturnsAsync(userStatEntity);
        _postcardDataRepositoryMock.Setup(repo => repo.TotalCountByUserId(validUserId)).ReturnsAsync(10);
        _userFriendsRepositoryMock.Setup(repo => repo.FollowersCount(validUserId)).ReturnsAsync(5);
        _userFriendsRepositoryMock.Setup(repo => repo.FollowingCount(validUserId)).ReturnsAsync(5);
        _mapperMock.Setup(mapper => mapper.Map<UserDto>(It.IsAny<User>())).Returns(new UserDto());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.GetUser(validUserId));
    }

    [Fact]
    public async Task GetPagination_ValidPaginationAndFilters_ShouldReturnPaginationResponse()
    {
        // Arrange
        var pagination = new PaginationRequest { PageNumber = 1, PageSize = 10 };
        var filters = new FiltersUserRequest { };
        var allUsers = new List<User> { };
        var users = new List<User> { };

        _userRepositoryMock.Setup(repo => repo.GetAllUsers(It.IsAny<FiltersUser>())).ReturnsAsync(allUsers);
        _userRepositoryMock.Setup(repo => repo.GetPaginationUsers(It.IsAny<Pagination>(), It.IsAny<FiltersUser>())).ReturnsAsync(users);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserDto>>(It.IsAny<IEnumerable<User>>())).Returns(new List<UserDto>());

        // Act
        var result = await _userService.GetPagination(pagination, filters);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginationResponse<UserDto>>(result);
    }
}