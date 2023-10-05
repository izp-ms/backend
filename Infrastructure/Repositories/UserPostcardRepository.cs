using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserPostcardRepository : Repository<UserPostcard>, IUserPostcardRepository
{
    private readonly DataContext _dataContext;

    public UserPostcardRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<UserPostcard> GetUserPostcardByPostcardId(int postcardId)
    {
        return await _dataContext.UserPostcards.FirstOrDefaultAsync(a => a.PostcardId == postcardId);
    }

    public async Task<IEnumerable<UserPostcard>> GetUserPostcardByUserId(int userId)
    {
        return await _dataContext.UserPostcards.Where(a => a.UserId == userId).ToListAsync();
    }
}
