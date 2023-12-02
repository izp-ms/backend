using Domain.Entities;
using Infrastructure.Models;

namespace Domain.Interfaces;

public interface IUserFriendsRepository : IRepository<UserFriends>
{
    Task<IEnumerable<UserFriends>> GetAllFollowing(FiltersUser filters);
    Task<IEnumerable<UserFriends>> GetAllFollowers(FiltersUser filters);
    Task<IEnumerable<UserFriends>> GetPaginatedFollowing(Pagination pagination, FiltersUser filters);
    Task<IEnumerable<UserFriends>> GetPaginatedFollowers(Pagination pagination, FiltersUser filters);
    Task<UserFriends> GetByUserIdAndFriendId(int userId, int friendId);
    Task<int> FollowersCount(int userId);
    Task<int> FollowingCount(int userId);
}
