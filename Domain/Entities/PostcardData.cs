namespace Domain.Entities;

public class PostcardData : BaseEntity
{
    public string ImageBase64 { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }

    public virtual IEnumerable<Postcard> Postcards { get; set; } = new List<Postcard>();
}
