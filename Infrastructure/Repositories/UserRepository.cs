using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
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
}
