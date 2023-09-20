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

    public async Task<IEnumerable<UserFriends>> GetFriends(int userId)
    {
        return await _dataContext.UserFriends.Where(uf => uf.UserId == userId).ToListAsync();
    }

    public async Task<UserFriends> GetByUserIdAndFriendId(int userId, int friendId)
    {
        return await _dataContext.UserFriends.FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FriendId == friendId);
    }
}
