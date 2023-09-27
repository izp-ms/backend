namespace Domain.Entities;

public class PostcardCollection : BaseEntity
{
    public int UserId { get; set; }
    public int PostcardDataId { get; set; }

    public virtual User User { get; set; }
    public virtual PostcardData PostcardData { get; set; }
}
