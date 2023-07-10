using Application.Constants;
using Application.Dto;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public Task<IEnumerable<User>> GetAll()
    {
        return _userRepository.GetAll();
    }

    public async Task<string> Login(LoginUserDto loginUserDto)
    {
        User storedUser = _userRepository.GetUserByEmail(loginUserDto.Email);

        if (storedUser is null)
        {
            throw new InvalidLoginException(loginUserDto.Email);
        }

        PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(storedUser, storedUser.Password, loginUserDto.Password);
        if (passwordResult != PasswordVerificationResult.Success)
        {
            throw new InvalidLoginException(loginUserDto.Email, loginUserDto.Password);
        }

        User userDataForClaims = _userRepository.GetUserByEmail(loginUserDto.Email);

        return _userRepository.Login(userDataForClaims);
    }

    public async Task<RegistrationResult> Register(RegisterUserDto registerUserDto)
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

        if (result == RegistrationResult.Success)
        {
            User newUser = _mapper.Map<User>(registerUserDto);

            string hashedPassword = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
            newUser.Password = hashedPassword;
            newUser.Role = "USER";
            newUser.CreatedAt = DateTime.UtcNow;

            await _userRepository.Insert(newUser);
        }

        return result;
    }
}
