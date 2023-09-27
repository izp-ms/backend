using Domain.Entities;

namespace Domain.Interfaces;

public interface IPostcardCollectionRepository : IRepository<PostcardCollection>
{
  Task<IEnumerable<PostcardCollection>> GetPostcardCollectionByUserId(int userId);
}
