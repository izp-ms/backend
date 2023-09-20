using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class PostcardDataRepository : Repository<PostcardData>, IPostcardDataRepository
{
    private readonly DataContext _dataContext;

    public PostcardDataRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}
