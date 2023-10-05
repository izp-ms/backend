using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FavouritePostcardRepository : Repository<FavouritePostcard>, IFavouritePostcardRepository
{
    private readonly DataContext _dataContext;

    public FavouritePostcardRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<FavouritePostcard>> GetFavouritePostcardByUserId(int userId)
    {
        return await _dataContext.FavouritePostcards.Where(x => x.UserId == userId).ToListAsync();
    }
}
