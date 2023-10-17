namespace Application.Requests;

public class PaginationRequest
{
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 10;
}
