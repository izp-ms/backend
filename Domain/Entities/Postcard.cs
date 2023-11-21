namespace Domain.Entities;

public class Postcard : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int PostcardDataId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsSent { get; set; }

    public virtual PostcardData PostcardData { get; set; }
    public IEnumerable<User> Users { get; set; } = new List<User>();
}
