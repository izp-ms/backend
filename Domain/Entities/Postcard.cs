namespace Domain.Entities;

public class Postcard : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int ImageId { get; set; }
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual PostcardData Image { get; set; }
    public List<User> Users { get; set; }
}
