using Application.Constants;
using Application.Dto;
using Application.Helpers;
using Application.Interfaces;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserStatsRepository _userStatsRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepository, IUserStatsRepository userStatsRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _userStatsRepository = userStatsRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public Task<IEnumerable<User>> GetAll()
    {
        return _userRepository.GetAll();
    }

    public Task<LoginResponse> Login(LoginUserDto loginUserDto)
    {
        User storedUser = _userRepository.GetUserByEmail(loginUserDto.Email) ?? throw new Exception("User not found");
        PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(storedUser, storedUser.Password, loginUserDto.Password);
        if (passwordResult != PasswordVerificationResult.Success)
        {
            throw new Exception("Password is incorrect");
        }

        User userDataForClaims = _userRepository.GetUserByEmail(loginUserDto.Email);
        LoginResponse loginResponse = new LoginResponse() { Token = _userRepository.Login(userDataForClaims) };
        return Task.FromResult(loginResponse);
    }

    public async Task<RegisterResponse> Register(RegisterUserDto registerUserDto)
    {
        RegistrationResult result = RegistrationResult.Success;

        if (registerUserDto.Password != registerUserDto.ConfirmPassword)
        {
            result = RegistrationResult.PasswordsDoNotMatch;
        }

        if (registerUserDto.Password.Length < UserServiceConstants.MinPasswordLength)
        {
            result = RegistrationResult.WeakPassword;
        }

        if (!EmailRegex.IsValidEmail(registerUserDto.Email))
        {
            result = RegistrationResult.IncorrectEmail;
        }

        bool isEmailInUse = _userRepository.IsEmailInUse(registerUserDto.Email);
        if (isEmailInUse)
        {
            result = RegistrationResult.EmailAlreadyExists;
        }

        if (result != RegistrationResult.Success)
        {
            throw new Exception(result.ToString());
        }

        User newUser = _mapper.Map<User>(registerUserDto);

        string hashedPassword = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
        newUser.Password = hashedPassword;
        newUser.Role = "USER";
        newUser.CreatedAt = DateTime.UtcNow;

        User user = await _userRepository.Insert(newUser);
        await _userStatsRepository.Insert(new UserStat() { Id = user.Id, PostcardsReceived = 0, PostcardsSent = 0, Score = 0 });
        RegisterResponse registerResponse = _mapper.Map<RegisterResponse>(user);
        return registerResponse;
    }

    public Task<User> DeleteUser(int userId)
    {
        User userToDelete = _userRepository.Get(userId).Result ?? throw new Exception(userId.ToString());
        return _userRepository.Delete(userToDelete);
    }
}
