using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class UserFriendsService : IUserFriendsService
{
    private readonly IUserFriendsRepository _userFriendsRepository;
    private readonly IMapper _mapper;

    public UserFriendsService(IUserFriendsRepository userFriendsRepository, IMapper mapper)
    {
        _userFriendsRepository = userFriendsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FriendDto>> GetFriends(int userId)
    {
        IEnumerable<UserFriends> userFriends = await _userFriendsRepository.GetFriends(userId);
        return _mapper.Map<IEnumerable<FriendDto>>(userFriends);
    }

    public async Task<FriendDto> AddNewFriend(UserFriendRequest addUserFriendRequest)
    {
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
