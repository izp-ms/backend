using Domain.Entities;

namespace Domain.Interfaces;

public interface IPostcardRepository : IRepository<Postcard>
{
    Task<IEnumerable<Postcard>> GetAllPostcardsByUserId(int userId);
    Task<IEnumerable<Postcard>> GetPaginationByUserId(int pageNumber, int pageSize, int userId);
}
