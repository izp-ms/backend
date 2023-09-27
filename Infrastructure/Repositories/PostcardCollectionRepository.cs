using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostcardCollectionRepository : Repository<PostcardCollection>, IPostcardCollectionRepository
{
    private readonly DataContext _dataContext;

    public PostcardCollectionRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<PostcardCollection>> GetPostcardCollectionByUserId(int userId)
    {
        return await _dataContext.PostcardCollection
            .Include(x => x.PostcardData)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }
}
