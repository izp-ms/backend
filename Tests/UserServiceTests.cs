namespace Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _passwordHasherMock = new Mock<IPasswordHasher<User>>();
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object, _passwordHasherMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllUsers()
    {
        // Arrange
        List<User> expectedUsers = new List<User> { new User(), new User() };
        _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedUsers);

        // Act
        IEnumerable<User> result = await _userService.GetAll();

        // Assert
        Assert.Equal(expectedUsers, result);
    }

    [Fact]
    public async Task Login_ValidCredentials_ShouldReturnToken()
    {
        // Arrange
        LoginUserDto? loginUserDto = new LoginUserDto { Email = "test@example.com", Password = "password" };
        User? storedUser = new User { Email = "test@example.com", Password = "hashedPassword" };
        _userRepositoryMock.Setup(repo => repo.GetUserByEmail(loginUserDto.Email)).Returns(storedUser);
        _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(storedUser, storedUser.Password, loginUserDto.Password))
            .Returns(PasswordVerificationResult.Success);
        _userRepositoryMock.Setup(repo => repo.Login(storedUser)).Returns("token");

        // Act
        string? result = await _userService.Login(loginUserDto);

        // Assert
        Assert.Equal("token", result);
    }

    [Fact]
    public async Task Login_InvalidEmail_ShouldThrowInvalidLoginException()
    {
        // Arrange
        LoginUserDto? loginUserDto = new LoginUserDto { Email = "invalid@example.com", Password = "password" };
        _userRepositoryMock.Setup(repo => repo.GetUserByEmail(loginUserDto.Email)).Returns((User)null);

        // Act and Assert
        await Assert.ThrowsAsync<InvalidLoginException>(() => _userService.Login(loginUserDto));
    }

    [Fact]
    public async Task Login_InvalidPassword_ShouldThrowInvalidLoginException()
    {
        // Arrange
        LoginUserDto? loginUserDto = new LoginUserDto { Email = "test@example.com", Password = "wrongPassword" };
        User? storedUser = new User { Email = "test@example.com", Password = "hashedPassword" };
        _userRepositoryMock.Setup(repo => repo.GetUserByEmail(loginUserDto.Email)).Returns(storedUser);
        _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(storedUser, storedUser.Password, loginUserDto.Password))
            .Returns(PasswordVerificationResult.Failed);

        // Act and Assert
        await Assert.ThrowsAsync<InvalidLoginException>(() => _userService.Login(loginUserDto));
    }

    [Fact]
    public async Task Register_ValidUser_ShouldReturnSuccess()
    {
        // Arrange
        RegisterUserDto? registerUserDto = new RegisterUserDto
        {
            Email = "test@example.com",
            Password = "password",
            ConfirmPassword = "password"
        };
        _userRepositoryMock.Setup(repo => repo.IsEmailInUse(registerUserDto.Email)).Returns(false);
        _mapperMock.Setup(mapper => mapper.Map<User>(registerUserDto)).Returns(new User());
        _passwordHasherMock.Setup(hasher => hasher.HashPassword(It.IsAny<User>(), registerUserDto.Password)).Returns("hashedPassword");
        _userRepositoryMock.Setup(repo => repo.Insert(It.IsAny<User>()));

        // Act
        RegistrationResult result = await _userService.Register(registerUserDto);

        // Assert
        Assert.Equal(RegistrationResult.Success, result);
    }

    [Fact]
    public async Task Register_PasswordsDoNotMatch_ShouldReturnPasswordsDoNotMatch()
    {
        // Arrange
        RegisterUserDto? registerUserDto = new RegisterUserDto
        {
            Email = "test@example.com",
            Password = "password",
            ConfirmPassword = "differentPassword"
        };

        // Act
        RegistrationResult result = await _userService.Register(registerUserDto);

        // Assert
        Assert.Equal(RegistrationResult.PasswordsDoNotMatch, result);
    }

    [Fact]
    public async Task Register_WeakPassword_ShouldReturnWeakPassword()
    {
        // Arrange
        RegisterUserDto? registerUserDto = new RegisterUserDto
        {
            Email = "test@example.com",
            Password = "12345",
            ConfirmPassword = "12345"
        };

        // Act
        RegistrationResult result = await _userService.Register(registerUserDto);

        // Assert
        Assert.Equal(RegistrationResult.WeakPassword, result);
    }

    [Fact]
    public async Task Register_IncorrectEmail_ShouldReturnIncorrectEmail()
    {
        // Arrange
        RegisterUserDto? registerUserDto = new RegisterUserDto
        {
            Email = "invalidemail",
            Password = "password",
            ConfirmPassword = "password"
        };

        // Act
        RegistrationResult result = await _userService.Register(registerUserDto);

        // Assert
        Assert.Equal(RegistrationResult.IncorrectEmail, result);
    }

    [Fact]
    public async Task Register_EmailAlreadyExists_ShouldReturnEmailAlreadyExists()
    {
        // Arrange
        RegisterUserDto? registerUserDto = new RegisterUserDto
        {
            Email = "existing@example.com",
            Password = "password",
            ConfirmPassword = "password"
        };
        _userRepositoryMock.Setup(repo => repo.IsEmailInUse(registerUserDto.Email)).Returns(true);

        // Act
        RegistrationResult result = await _userService.Register(registerUserDto);

        // Assert
        Assert.Equal(RegistrationResult.EmailAlreadyExists, result);
    }

    [Fact]
    public async Task Register_SuccessfulRegistration_ShouldReturnSuccess()
    {
        // Arrange
        RegisterUserDto? registerUserDto = new RegisterUserDto
        {
            Email = "test@example.com",
            Password = "password",
            ConfirmPassword = "password"
        };
        _userRepositoryMock.Setup(repo => repo.IsEmailInUse(registerUserDto.Email)).Returns(false);
        _mapperMock.Setup(mapper => mapper.Map<User>(registerUserDto)).Returns(new User());
        _passwordHasherMock.Setup(hasher => hasher.HashPassword(It.IsAny<User>(), registerUserDto.Password)).Returns("hashedPassword");
        _userRepositoryMock.Setup(repo => repo.Insert(It.IsAny<User>()));

        // Act
        RegistrationResult result = await _userService.Register(registerUserDto);

        // Assert
        Assert.Equal(RegistrationResult.Success, result);
    }
}