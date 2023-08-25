namespace Domain.Entities;

public class Address : BaseEntity
{
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }

    public virtual User User { get; set; }
}
