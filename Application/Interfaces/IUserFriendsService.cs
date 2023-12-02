using Application.Dto;
using Application.Requests;

namespace Application.Interfaces;

public interface IUserFriendsService
{
    Task<IEnumerable<FriendDto>> GetFollowing(int userId);
    Task<IEnumerable<FriendDto>> GetFollowers(int userId);
    Task<bool> IsFollowing(int userId, int friendId);
    Task<FriendDto> AddNewFriend(UserFriendRequest addUserFriendRequest);
    Task<FriendDto> DeleteFriend(UserFriendRequest deleteUserFriendRequest);
}
