using Domain.Entities;
using Infrastructure.Models;

namespace Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    bool IsEmailInUse(string email);
    User GetUserByEmail(string email);
    string Login(User user);
    Task<IEnumerable<User>> GetAllUsers(FiltersUser filters);
    Task<IEnumerable<User>> GetPaginationUsers(Pagination pagination, FiltersUser filters);
}
