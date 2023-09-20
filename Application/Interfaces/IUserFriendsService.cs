using Application.Dto;

namespace Application.Interfaces;

public interface IUserFriendsService
{
    Task<IEnumerable<FriendDto>> GetFriends(int userId);
    Task<FriendDto> AddNewFriend(UserFriendRequest addUserFriendRequest);
    Task<FriendDto> DeleteFriend(UserFriendRequest deleteUserFriendRequest);
}
