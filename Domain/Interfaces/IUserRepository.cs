using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    bool IsEmailInUse(string email);
    User GetUserByEmail(string email);
    string Login(User user);
}
