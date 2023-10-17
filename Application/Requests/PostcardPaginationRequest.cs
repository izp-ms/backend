namespace Application.Requests;

public class PaginatedPostcardDataRequest : PaginationRequest
{
  public int? UserId { get; set; }
}
