namespace Domain.Entities;

public class Postcard : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int ImageId { get; set; }
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual PostcardImage Image { get; set; }
    public virtual IEnumerable<UserPostcard> UserPostcards { get; set; } = new List<UserPostcard>();
}
