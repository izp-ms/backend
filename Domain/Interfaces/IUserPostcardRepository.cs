using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserPostcardRepository : IRepository<UserPostcard>
{
    Task<UserPostcard> GetUserPostcardByPostcardId(int postcardId);
    Task<IEnumerable<UserPostcard>> GetUserPostcardByUserId(int userId);
}
