using Application.Requests;
using Infrastructure.Models;

namespace Application.Mappings.Manual;

public static class PaginationMapper
{
    public static Pagination Map(PaginationRequest request)
    {
        return new Pagination
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
