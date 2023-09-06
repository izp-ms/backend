using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserPostcardRepository : Repository<UserPostcard>, IUserPostcardRepository
{
    private readonly DataContext _dataContext;

    public UserPostcardRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}
