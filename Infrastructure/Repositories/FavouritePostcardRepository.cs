using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class FavouritePostcardRepository : Repository<FavouritePostcard>, IFavouritePostcardRepository
{
    private readonly DataContext _dataContext;

    public FavouritePostcardRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}
