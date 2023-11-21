namespace Application.Requests;

public class FiltersPostcardDataRequest
{
    public string Search { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public int? CollectRangeInMeters { get; set; }
    public string Type { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public int? UserId { get; set; }
    public string OrderBy { get; set; }
}
