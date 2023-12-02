using Application.Dto;
using Application.Interfaces;
using Application.Mappings.Manual;
using Application.Requests;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class UserFriendsService : IUserFriendsService
{
    private readonly IUserFriendsRepository _userFriendsRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserFriendsService(
        IUserFriendsRepository userFriendsRepository,
        IUserService userService,
        IMapper mapper
    )
    {
        _userFriendsRepository = userFriendsRepository;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<FriendDto>> GetPaginatedFollowing(PaginationRequest pagination, FiltersUserRequest filters)
    {
        IEnumerable<UserFriends> allUserFollowing = await _userFriendsRepository.GetAllFollowing(FiltersMapper.Map(filters));
        IEnumerable<UserFriends> userFollowing = await _userFriendsRepository.GetPaginatedFollowing(PaginationMapper.Map(pagination), FiltersMapper.Map(filters));

        IEnumerable<FriendDto> mappedFollowing = FriendsMapper.MapFollowing(userFollowing);

        int totalPages = (int)Math.Ceiling(allUserFollowing.Count() / (double)pagination.PageSize);

        PaginationResponse<FriendDto> paginationResponse = new PaginationResponse<FriendDto>()
        {
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize,
            TotalCount = allUserFollowing.Count(),
            TotalPages = totalPages,
            Content = mappedFollowing
        };

        return paginationResponse;
    }

    public async Task<PaginationResponse<FriendDto>> GetPaginatedFollowers(PaginationRequest pagination, FiltersUserRequest filters)
    {
        IEnumerable<UserFriends> allUserFollowers = await _userFriendsRepository.GetAllFollowers(FiltersMapper.Map(filters));
        IEnumerable<UserFriends> userFollowers = await _userFriendsRepository.GetPaginatedFollowers(PaginationMapper.Map(pagination), FiltersMapper.Map(filters));

        IEnumerable<FriendDto> mappedFollowers = FriendsMapper.MapFollowers(userFollowers);

        int totalPages = (int)Math.Ceiling(allUserFollowers.Count() / (double)pagination.PageSize);

        PaginationResponse<FriendDto> paginationResponse = new PaginationResponse<FriendDto>()
        {
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize,
            TotalCount = allUserFollowers.Count(),
            TotalPages = totalPages,
            Content = mappedFollowers
        };

        return paginationResponse;
    }

    public async Task<bool> IsFollowing(int userId, int friendId)
    {
        UserFriends userFriends = await _userFriendsRepository.GetByUserIdAndFriendId(userId, friendId);
        return userFriends != null;
    }

    public async Task<FriendDto> AddNewFriend(UserFriendRequest addUserFriendRequest)
    {
        if (!await _userService.IsUserActive(addUserFriendRequest.FriendId))
        {
            throw new Exception("User is not active");
        }

        if (await _userFriendsRepository.GetByUserIdAndFriendId(addUserFriendRequest.UserId, addUserFriendRequest.FriendId) != null)
        {
            throw new Exception("User friend already exists");
        }

        UserFriends userFriends = await _userFriendsRepository.Insert(new UserFriends
        {
            UserId = addUserFriendRequest.UserId,
            FriendId = addUserFriendRequest.FriendId
        });
        return _mapper.Map<FriendDto>(userFriends);
    }

    public async Task<FriendDto> DeleteFriend(UserFriendRequest deleteUserFriendRequest)
    {
        UserFriends userFriend = await _userFriendsRepository.GetByUserIdAndFriendId(deleteUserFriendRequest.UserId, deleteUserFriendRequest.FriendId);
        if (userFriend == null)
        {
            throw new Exception("User friend not found");
        }
        await _userFriendsRepository.Delete(userFriend);
        return _mapper.Map<FriendDto>(userFriend);
    }
}
