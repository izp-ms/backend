namespace Application.Requests;

public class TransferPostcardRequest
{
  public PostcardDto PostcardDto { get; set; }
  public int NewUserId { get; set; }
}
