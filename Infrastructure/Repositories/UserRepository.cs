using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly DataContext _dataContext;
    private readonly AuthenticationSettings _authenticationSettings;

    public UserRepository(DataContext dataContext, AuthenticationSettings authenticationSettings) : base(dataContext)
    {
        _dataContext = dataContext;
        _authenticationSettings = authenticationSettings;
    }

    public async Task<IEnumerable<User>> GetAllUsers(FiltersUser filters)
    {
        IQueryable<User> query = _dataContext.Users
            .Include(x => x.UsersDetails)
            .Include(x => x.UsersStats)
            .Include(x => x.Address)
            .Where(x => x.IsActive == true);

        query = ApplyFilters(query, filters);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<User>> GetPaginationUsers(Pagination pagination, FiltersUser filters)
    {
        PaginationValidator.CheckPaginationValid(pagination.PageNumber, pagination.PageSize);

        IQueryable<User> query = _dataContext.Users
            .Include(x => x.UsersDetails)
            .Include(x => x.UsersStats)
            .Include(x => x.Address)
            .Where(x => x.IsActive == true);

        query = ApplyFilters(query, filters);

        return await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public User GetUserByEmail(string email)
    {
        return _dataContext.Users.FirstOrDefault((User user) => user.Email.Equals(email));
    }

    public bool IsEmailInUse(string email)
    {
        foreach (User user in _dataContext.Users)
        {
            if (user.Email.Equals(email))
            {
                return true;
            }
        }

        return false;
    }

    public string Login(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.NickName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        DateTime expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        JwtSecurityToken token = new JwtSecurityToken(
            _authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: credentials
        );

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private static IQueryable<User> ApplyFilters(IQueryable<User> query, FiltersUser filters)
    {
        if (!string.IsNullOrEmpty(filters.Search))
        {
            query = query.Where(x => x.NickName.ToLower().Contains(filters.Search.ToLower()));
        }

        if (!string.IsNullOrEmpty(filters.NickName))
        {
            query = query.Where(x => x.NickName.ToLower() == filters.NickName.ToLower());
        }

        if (!string.IsNullOrEmpty(filters.Email))
        {
            query = query.Where(x => x.Email.ToLower() == filters.Email.ToLower());
        }

        if (filters.CreatedFrom.HasValue)
        {
            query = query.Where(x => x.CreatedAt >= filters.CreatedFrom);
        }

        if (filters.CreatedTo.HasValue)
        {
            query = query.Where(x => x.CreatedAt <= filters.CreatedTo);
        }

        if (!string.IsNullOrEmpty(filters.OrderBy))
        {
            query = OrderBy(query, filters.OrderBy);
        }

        if (filters.UserId != 0)
        {
            query = query.Where(x => x.Id != filters.UserId);
        }

        return query;
    }

    private static IQueryable<User> OrderBy(IQueryable<User> query, string orderBy)
    {
        string direction = orderBy.StartsWith("-") ? "desc" : "asc";
        string property = orderBy.Replace("-", "");

        query = property switch
        {
            "nickName" => direction == "asc" ? query.OrderBy(x => x.NickName) : query.OrderByDescending(x => x.NickName),
            "email" => direction == "asc" ? query.OrderBy(x => x.Email) : query.OrderByDescending(x => x.Email),
            "createdAt" => direction == "asc" ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Id),
        };

        return query;
    }
}
