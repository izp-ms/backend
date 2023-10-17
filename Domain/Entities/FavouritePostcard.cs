namespace Domain.Entities;

public class FavouritePostcard : BaseEntity
{
    public int UserId { get; set; }
    public int PostcardId { get; set; }
    public int Order { get; set; }

    public virtual User User { get; set; }
    public virtual Postcard Postcard { get; set; }
}
