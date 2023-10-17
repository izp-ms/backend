using Domain.Entities;
using Infrastructure.Models;

namespace Domain.Interfaces;

public interface IPostcardRepository : IRepository<Postcard>
{
    Task<IEnumerable<Postcard>> GetAllPostcardsByUserId(FiltersPostcard filters);
    Task<IEnumerable<Postcard>> GetPaginationByUserId(Pagination pagination, FiltersPostcard filters);
}
