using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

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
        var x = _dataContext.UserPostcards.Where(a => a.PostcardId == postcardId).FirstOrDefault();
        //var xd = await _dataContext.UserPostcards.FirstAsync(x => x.PostcardId == postcardId);
        return x;
    }
}
