namespace Application.Requests;

public class FiltersUserRequest
{
    public string Search { get; set; }
    public string Email { get; set; }
    public string NickName { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public string OrderBy { get; set; }
}
