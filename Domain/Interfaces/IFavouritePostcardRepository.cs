using Domain.Entities;

namespace Domain.Interfaces;

public interface IFavouritePostcardRepository : IRepository<FavouritePostcard>
{
    Task<IEnumerable<FavouritePostcard>> GetFavouritePostcardByUserId(int userId);
}
