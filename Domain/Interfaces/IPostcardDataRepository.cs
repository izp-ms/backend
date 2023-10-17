using Domain.Entities;
using Infrastructure.Models;

namespace Domain.Interfaces;

public interface IPostcardDataRepository : IRepository<PostcardData>
{
    Task<IEnumerable<PostcardData>> GetAllPostcardsData(FiltersPostcardData filters);
    Task<IEnumerable<PostcardData>> GetPagination(Pagination pagination, FiltersPostcardData filters);
}
