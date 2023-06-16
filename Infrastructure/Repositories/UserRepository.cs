using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public bool IsEmailInUse(string email)
    {
        foreach (var user in _dataContext.Users)
        {
            if (user.Email.Equals(email))
            {
                return true;
            }
        }

        return false;
    }

}
