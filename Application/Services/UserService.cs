using Application.Constants;
using Application.Dto;
using Application.Helpers;
using Application.Interfaces;
using Application.Mappings.Manual;
using Application.Requests;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;
    private readonly IUserStatsRepository _userStatsRepository;
    private readonly IUserDetailRepository _userDetailRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IUserFriendsRepository _userFriendsRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        IUserContextService contextService,
        IUserRepository userRepository,
        IUserStatsRepository userStatsRepository,
        IUserDetailRepository userDetailRepository,
        IAddressRepository addressRepository,
        IUserFriendsRepository userFriendsRepository,
        IMapper mapper,
        IPasswordHasher<User> passwordHasher
        )
    {
        _userContextService = contextService;
        _userRepository = userRepository;
        _userStatsRepository = userStatsRepository;
        _userDetailRepository = userDetailRepository;
        _addressRepository = addressRepository;
        _userFriendsRepository = userFriendsRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> GetUser(int userId)
    {
        if (!await IsUserActive(userId))
        {
            throw new Exception("User is not active");
        }

        User user = await _userRepository.Get(userId) ?? throw new Exception("User not found");
        Address address = await _addressRepository.Get(userId) ?? throw new Exception("Address not found");
        UserDetail userDetail = await _userDetailRepository.Get(userId) ?? throw new Exception("User detail not found");
        UserStat userStat = await _userStatsRepository.Get(userId) ?? throw new Exception("User stat not found");

        UserDto userDto = _mapper.Map<UserDto>(user);
        _mapper.Map(address, userDto);
        _mapper.Map(userDetail, userDto);
        _mapper.Map(userStat, userDto);

        int postcardsCount = user.Postcards.Count();
        IEnumerable<UserFriends> followersUsers = await _userFriendsRepository.GetFollowers(userId);
        int followersCount = followersUsers.Count();
        IEnumerable<UserFriends> followingUsers = await _userFriendsRepository.GetFollowing(userId);
        int followingCount = followingUsers.Count();

        userDto.PostcardsCount = postcardsCount;
        userDto.FollowersCount = followersCount;
        userDto.FollowingCount = followingCount;

        return userDto;
    }

    public async Task<PaginationResponse<UserDto>> GetPagination(PaginationRequest pagination, FiltersUserRequest filters)
    {
        IEnumerable<User> allUsers = await _userRepository.GetAllUsers(FiltersMapper.Map(filters));
        IEnumerable<User> users = await _userRepository.GetPaginationUsers(PaginationMapper.Map(pagination), FiltersMapper.Map(filters));

        IEnumerable<UserDto> mappedUsers = _mapper.Map<IEnumerable<UserDto>>(users);

        int totalPages = (int)Math.Ceiling(allUsers.Count() / (double)pagination.PageSize);

        PaginationResponse<UserDto> paginationResponse = new PaginationResponse<UserDto>()
        {
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize,
            TotalCount = allUsers.Count(),
            TotalPages = totalPages,
            Content = mappedUsers
        };

        return paginationResponse;
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

    public async Task<User> DeactivateUser(int userId)
    {
        User user = await _userRepository.Get(userId) ?? throw new Exception("User not found");
        user.IsActive = false;
        await _userRepository.Update(user);
        return user;
    }

    public async Task<bool> IsUserActive(int userId)
    {
        User user = await _userRepository.Get(userId);
        if (user == null)
        {
            return false;
        }
        return user.IsActive;
    }

    public async Task<UserUpdateDto> UpdateUser(UserUpdateDto userUpdateDto)
    {
        if (!await IsUserActive(userUpdateDto.Id) == false)
        {
            throw new Exception("User is not active");
        }

        User user = await _userRepository.Get(userUpdateDto.Id) ?? throw new Exception("User not found");
        UserDetail userDetail = await _userDetailRepository.Get(userUpdateDto.Id) ?? throw new Exception("User detail not found");
        Address address = await _addressRepository.Get(userUpdateDto.Id) ?? throw new Exception("Address not found");

        _mapper.Map(userUpdateDto, user);
        _mapper.Map(userUpdateDto, userDetail);
        _mapper.Map(userUpdateDto, address);

        await _userRepository.Update(user);
        await _userDetailRepository.Update(userDetail);
        await _addressRepository.Update(address);

        return userUpdateDto;
    }
}
