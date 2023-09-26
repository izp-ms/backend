using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserFriendsRepository : IRepository<UserFriends>
{
    Task<IEnumerable<UserFriends>> GetFollowing(int userId);
    Task<IEnumerable<UserFriends>> GetFollowers(int userId);
    Task<UserFriends> GetByUserIdAndFriendId(int userId, int friendId);
}
