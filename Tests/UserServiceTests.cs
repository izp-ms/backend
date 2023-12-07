using Application.Dto;
using Application.Response;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

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
        _userService = new UserService(
            contextService: null,
            userRepository: _userRepositoryMock.Object,
            userStatsRepository: null,
            userDetailRepository: null,
            addressRepository: null,
            userFriendsRepository: null,
            postcardDataRepository: null,
            mapper: _mapperMock.Object,
            passwordHasher: _passwordHasherMock.Object
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
}