using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserFriendsRepository : IRepository<UserFriends>
{
  Task<IEnumerable<UserFriends>> GetFriends(int userId);
  Task<UserFriends> GetByUserIdAndFriendId(int userId, int friendId);
}
