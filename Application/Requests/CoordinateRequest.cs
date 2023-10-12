namespace Application.Dto;

public class CoordinateRequest
{
  public string Latitude { get; set; }
  public string Longitude { get; set; }
  public int PostcardNotificationRangeInMeters { get; set; } = 1000;
}
