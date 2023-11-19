namespace Domain.Entities;

public class User : BaseEntity
{
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }

    public virtual UserDetail UsersDetails { get; set; } = new UserDetail();
    public virtual UserStat UsersStats { get; set; } = new UserStat();
    public virtual Address Address { get; set; } = new Address();
    public virtual IEnumerable<Postcard> Postcards { get; set; } = new List<Postcard>();
    public virtual IEnumerable<UserFriends> Friends { get; set; } = new List<UserFriends>();
}
