using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserFriendsRepository : Repository<UserFriends>, IUserFriendsRepository
{
    private readonly DataContext _dataContext;

    public UserFriendsRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<UserFriends>> GetFollowing(int userId)
    {
        return await _dataContext.UserFriends
            .Include(uf => uf.Friend)
            .Include(uf => uf.Friend.Address)
            .Include(uf => uf.Friend.UsersDetails)
            .Include(uf => uf.Friend.UsersStats)
            .Where(uf => uf.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<UserFriends>> GetFollowers(int userId)
    {
        IQueryable<int> friendsIds = _dataContext.UserFriends
            .Select(uf => uf.UserId);

        return await _dataContext.UserFriends
            .Include(uf => uf.User)
            .Include(uf => uf.User.Address)
            .Include(uf => uf.User.UsersDetails)
            .Include(uf => uf.User.UsersStats)
            .Where(uf => friendsIds.Contains(uf.UserId))
            .ToListAsync();
    }

    public async Task<UserFriends> GetByUserIdAndFriendId(int userId, int friendId)
    {
        return await _dataContext.UserFriends.FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FriendId == friendId);
    }
}
