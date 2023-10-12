using Domain.Entities;

namespace Domain.Interfaces;

public interface IPostcardDataRepository : IRepository<PostcardData>
{
    Task<IEnumerable<PostcardData>> GetAllPostcardsDataByUserId(int userId);
    Task<IEnumerable<PostcardData>> GetPaginationByUserId(int pageNumber, int pageSize, int userId);
}
