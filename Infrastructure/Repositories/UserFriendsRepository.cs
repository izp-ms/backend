using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserFriendsRepository : Repository<UserFriends>, IUserFriendsRepository
{
    private readonly DataContext _dataContext;

    public UserFriendsRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<UserFriends>> GetAllFollowing(FiltersUser filters)
    {
        IQueryable<UserFriends> query = _dataContext.UserFriends
            .Include(uf => uf.Friend)
            .Include(uf => uf.Friend.Address)
            .Include(uf => uf.Friend.UsersDetails)
            .Include(uf => uf.Friend.UsersStats)
            .Where(uf => uf.UserId == filters.UserId);

        query = ApplyFilters(query, filters);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<UserFriends>> GetAllFollowers(FiltersUser filters)
    {
        List<int> friendsIds = await _dataContext.UserFriends
            .Where(uf => uf.UserId == filters.UserId)
            .Select(uf => uf.UserId)
            .ToListAsync();

        IQueryable<UserFriends> query = _dataContext.UserFriends
            .Include(uf => uf.User)
            .Include(uf => uf.User.Address)
            .Include(uf => uf.User.UsersDetails)
            .Include(uf => uf.User.UsersStats)
            .Where(uf => friendsIds.Contains(uf.FriendId));

        query = ApplyFilters(query, filters);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<UserFriends>> GetPaginatedFollowing(Pagination pagination, FiltersUser filters)
    {
        PaginationValidator.CheckPaginationValid(pagination.PageNumber, pagination.PageSize);

        IQueryable<UserFriends> query = _dataContext.UserFriends
            .Include(uf => uf.Friend)
            .Include(uf => uf.Friend.Address)
            .Include(uf => uf.Friend.UsersDetails)
            .Include(uf => uf.Friend.UsersStats)
            .Where(uf => uf.UserId == filters.UserId);

        query = ApplyFilters(query, filters);

        return await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserFriends>> GetPaginatedFollowers(Pagination pagination, FiltersUser filters)
    {
        PaginationValidator.CheckPaginationValid(pagination.PageNumber, pagination.PageSize);

        List<int> friendsIds = await _dataContext.UserFriends
            .Where(uf => uf.UserId == filters.UserId)
            .Select(uf => uf.UserId)
            .ToListAsync();

        IQueryable<UserFriends> query = _dataContext.UserFriends
            .Include(uf => uf.User)
            .Include(uf => uf.User.Address)
            .Include(uf => uf.User.UsersDetails)
            .Include(uf => uf.User.UsersStats)
            .Where(uf => friendsIds.Contains(uf.FriendId));

        query = ApplyFilters(query, filters);

        return await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public async Task<UserFriends> GetByUserIdAndFriendId(int userId, int friendId)
    {
        return await _dataContext.UserFriends.FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FriendId == friendId);
    }

    public async Task<int> FollowersCount(int userId)
    {
        return await _dataContext.UserFriends.Where(uf => uf.FriendId == userId).CountAsync();
    }

    public async Task<int> FollowingCount(int userId)
    {
        return await _dataContext.UserFriends.Where(uf => uf.UserId == userId).CountAsync();
    }

    private static IQueryable<UserFriends> ApplyFiltersForAllRecords(IQueryable<UserFriends> query, FiltersUser filters)
    {
        if (!string.IsNullOrEmpty(filters.Search))
        {
            query = query.Where(x => x.Friend.NickName.ToLower().Contains(filters.Search.ToLower()));
        }

        if (!string.IsNullOrEmpty(filters.NickName))
        {
            query = query.Where(x => x.Friend.NickName.ToLower() == filters.NickName.ToLower());
        }

        if (!string.IsNullOrEmpty(filters.Email))
        {
            query = query.Where(x => x.Friend.Email.ToLower() == filters.Email.ToLower());
        }

        if (filters.CreatedFrom.HasValue)
        {
            query = query.Where(x => x.Friend.CreatedAt >= filters.CreatedFrom);
        }

        if (filters.CreatedTo.HasValue)
        {
            query = query.Where(x => x.Friend.CreatedAt <= filters.CreatedTo);
        }

        if (!string.IsNullOrEmpty(filters.OrderBy))
        {
            query = OrderBy(query, filters.OrderBy);
        }

        if (filters.UserId != 0)
        {
            query = query.Where(x => x.Id != filters.UserId);
        }

        return query;
    }

    private static IQueryable<UserFriends> ApplyFilters(IQueryable<UserFriends> query, FiltersUser filters)
    {
        if (!string.IsNullOrEmpty(filters.Search))
        {
            query = query.Where(x => x.Friend.NickName.ToLower().Contains(filters.Search.ToLower()));
        }

        if (!string.IsNullOrEmpty(filters.NickName))
        {
            query = query.Where(x => x.Friend.NickName.ToLower() == filters.NickName.ToLower());
        }

        if (!string.IsNullOrEmpty(filters.Email))
        {
            query = query.Where(x => x.Friend.Email.ToLower() == filters.Email.ToLower());
        }

        if (filters.CreatedFrom.HasValue)
        {
            query = query.Where(x => x.Friend.CreatedAt >= filters.CreatedFrom);
        }

        if (filters.CreatedTo.HasValue)
        {
            query = query.Where(x => x.Friend.CreatedAt <= filters.CreatedTo);
        }

        if (!string.IsNullOrEmpty(filters.OrderBy))
        {
            query = OrderBy(query, filters.OrderBy);
        }

        return query;
    }

    private static IQueryable<UserFriends> OrderBy(IQueryable<UserFriends> query, string orderBy)
    {
        string direction = orderBy.StartsWith("-") ? "desc" : "asc";
        string property = orderBy.Replace("-", "");

        query = property switch
        {
            "nickName" => direction == "asc" ? query.OrderBy(x => x.Friend.NickName) : query.OrderByDescending(x => x.Friend.NickName),
            "email" => direction == "asc" ? query.OrderBy(x => x.Friend.Email) : query.OrderByDescending(x => x.Friend.Email),
            "createdAt" => direction == "asc" ? query.OrderBy(x => x.Friend.CreatedAt) : query.OrderByDescending(x => x.Friend.CreatedAt),
            _ => query.OrderBy(x => x.Id),
        };

        return query;
    }
}
