namespace Domain.Entities;

public class UserDetail : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string AvatarBase64 { get; set; }
    public string Description { get; set; }

    public virtual User User { get; set; }
}
