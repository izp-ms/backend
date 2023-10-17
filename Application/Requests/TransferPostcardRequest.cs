namespace Application.Requests;

public class TransferPostcardRequest
{
  public int PostcardId { get; set; }
  public int NewUserId { get; set; }
}
