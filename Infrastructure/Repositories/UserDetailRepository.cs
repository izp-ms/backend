using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserDetailRepository : Repository<UserDetail>, IUserDetailRepository
{
    private readonly DataContext _dataContext;

    public UserDetailRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}
