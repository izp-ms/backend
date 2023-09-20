using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class PostcardRepository : Repository<Postcard>, IPostcardRepository
{
    private readonly DataContext _dataContext;

    public PostcardRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}
