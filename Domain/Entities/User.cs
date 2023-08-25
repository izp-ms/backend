namespace Domain.Entities;

public class User : BaseEntity
{
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual UserDetail UsersDetails { get; set; }
    public virtual UserStat UsersStats { get; set; }
    public virtual Address Address { get; set; }
    public virtual IEnumerable<UserPostcard> UserPostcards { get; set; } = new List<UserPostcard>();
}
