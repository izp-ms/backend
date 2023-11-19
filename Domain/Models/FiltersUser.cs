namespace Infrastructure.Models;

public class FiltersUser
{
    public string Search { get; set; }
    public string Email { get; set; }
    public string NickName { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public string OrderBy { get; set; }
}
