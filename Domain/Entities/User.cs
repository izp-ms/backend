namespace Domain.Entities;

public class User : BaseEntity
{
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
}
