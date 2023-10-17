using Application.Requests;
using Infrastructure.Models;

namespace Application.Mappings.Manual;

public static class FiltersMapper
{
    public static FiltersPostcard Map(FiltersPostcardRequest request)
    {
        return new FiltersPostcard
        {
            Search = request.Search,
            Type = request.Type,
            IsSent = request.IsSent,
            UserId = request.UserId,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
            OrderBy = request.OrderBy
        };
    }
}
