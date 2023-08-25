namespace Domain.Entities;

public class UserStat : BaseEntity
{
    public int PostcardsSent { get; set; }
    public int PostcardsReceived { get; set; }
    public int Score { get; set; }

    public virtual User User { get; set; }
}
