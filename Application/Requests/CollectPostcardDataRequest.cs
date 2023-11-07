using Application.Dto;

namespace Application.Requests;

public class CollectPostcardDataRequest
{
  public CoordinateRequest CoordinateRequest { get; set; }
  public int PostcardDataId { get; set; }
}
