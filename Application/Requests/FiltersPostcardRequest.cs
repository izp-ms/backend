namespace Application.Requests;

public class FiltersPostcardRequest
{
    public string Search { get; set; }
    public string Type { get; set; }
    public bool? IsSent { get; set; }
    public int UserId { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string OrderBy { get; set; }
}
