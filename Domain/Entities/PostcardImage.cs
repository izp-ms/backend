namespace Domain.Entities;

public class PostcardImage : BaseEntity
{
    public string ImageBase64 { get; set; }

    public virtual IEnumerable<Postcard> Postcards { get; set; } = new List<Postcard>();
}
