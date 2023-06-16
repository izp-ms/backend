using Application.Constants;
using Application.Dto;
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

    public async Task<string> Login(LoginUserDto loginUserDto)
    {
        throw new NotImplementedException();
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
            newUser.Role = Role.User;

            await _userRepository.Insert(newUser);
        }

        return result;
    }
}
