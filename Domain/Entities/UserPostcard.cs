namespace Domain.Entities;

public class UserPostcard : BaseEntity
{
    public int UserId { get; set; }
    public int PostcardId { get; set; }
    public DateTime ReceivedAt { get; set; }

    public virtual User User { get; set; }
    public virtual Postcard Postcard { get; set; }
}
