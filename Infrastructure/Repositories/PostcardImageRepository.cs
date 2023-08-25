using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class PostcardImageRepository : Repository<PostcardImage>, IPostcardImageRepository
{
    private readonly DataContext _dataContext;

    public PostcardImageRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}
