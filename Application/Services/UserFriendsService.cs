using Application.Dto;
using Application.Interfaces;
using Application.Mappings.Manual;
using Application.Requests;
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

    public async Task<IEnumerable<FriendDto>> GetFollowing(int userId)
    {
        IEnumerable<UserFriends> userFriends = await _userFriendsRepository.GetFollowing(userId);
        return FriendsMapper.Map(userFriends);
    }

    public async Task<IEnumerable<FriendDto>> GetFollowers(int userId)
    {
        IEnumerable<UserFriends> userFriends = await _userFriendsRepository.GetFollowers(userId);
        return FriendsMapper.Map(userFriends);
    }

    public async Task<FriendDto> AddNewFriend(UserFriendRequest addUserFriendRequest)
    {
        if (!await _userService.IsUserActive(addUserFriendRequest.FriendId))
        {
            throw new Exception("User is not active");
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
