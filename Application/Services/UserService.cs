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
using System.Transactions;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;
    private readonly IUserStatsRepository _userStatsRepository;
    private readonly IUserDetailRepository _userDetailRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        IUserContextService contextService,
        IUserRepository userRepository,
        IUserStatsRepository userStatsRepository,
        IUserDetailRepository userDetailRepository,
        IAddressRepository addressRepository,
        IMapper mapper,
        IPasswordHasher<User> passwordHasher
        )
    {
        _userContextService = contextService;
        _userRepository = userRepository;
        _userStatsRepository = userStatsRepository;
        _userDetailRepository = userDetailRepository;
        _addressRepository = addressRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> GetUser()
    {
        User user = await _userRepository.Get(_userContextService.GetUserId ?? 0) ?? throw new Exception("User not found");
        Address address = await _addressRepository.Get(user.Id) ?? throw new Exception("Address not found");
        UserDetail userDetail = await _userDetailRepository.Get(user.Id) ?? throw new Exception("User detail not found");
        UserStat userStat = await _userStatsRepository.Get(user.Id) ?? throw new Exception("User stat not found");

        UserDto userDto = _mapper.Map<UserDto>(user);
        _mapper.Map(address, userDto);
        _mapper.Map(userDetail, userDto);
        _mapper.Map(userStat, userDto);
        return userDto;
        // eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjI1IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InN0cmluZyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InN0cmluZzY1NEBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVU0VSIiwiZXhwIjoxNjk1MTM2NzU2LCJpc3MiOiJodHRwOi8vcG9zdGx5LmNvbSIsImF1ZCI6Imh0dHA6Ly9wb3N0bHkuY29tIn0.xbi5GizQoqFNK2r5R5ZEpYWauRDR05zEpTgeI_e_sXM
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
        await _addressRepository.Insert(new Address() { Id = user.Id });
        await _userDetailRepository.Insert(new UserDetail() { Id = user.Id });
        RegisterResponse registerResponse = _mapper.Map<RegisterResponse>(user);
        return registerResponse;
    }

    public async Task<User> DeleteUser(int userId)
    {
        using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            User userToDelete = await _userRepository.Get(userId) ?? throw new Exception(userId.ToString());
            UserStat userStatToDelete = await _userStatsRepository.Get(userId) ?? throw new Exception(userId.ToString());
            await _userStatsRepository.Delete(userStatToDelete);

            UserDetail userDetailToDelete = await _userDetailRepository.Get(userId) ?? throw new Exception(userId.ToString());
            await _userDetailRepository.Delete(userDetailToDelete);

            Address addressToDelete = await _addressRepository.Get(userId) ?? throw new Exception(userId.ToString());
            await _addressRepository.Delete(addressToDelete);

            await _userRepository.Delete(userToDelete);

            transactionScope.Complete();

            return userToDelete;
        }
        catch
        {
            throw;
        }
    }
}
