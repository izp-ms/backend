using Application.Dto;

namespace Application.Response;

public class CurrentLocationPostcardsResponse
{
  public IEnumerable<PostcardDataDto> PostcardsNearby { get; set; }
  public IEnumerable<PostcardDataDto> PostcardsCollected { get; set; }
}
