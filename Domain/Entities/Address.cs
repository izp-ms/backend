namespace Domain.Entities;

public class Address : BaseEntity
{
    public string City { get; set; }
    public string Country { get; set; }

    public virtual User User { get; set; }
}
