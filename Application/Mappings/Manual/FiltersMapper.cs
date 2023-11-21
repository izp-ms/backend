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
            IsSent = request.IsSent,
            UserId = request.UserId,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
            OrderBy = request.OrderBy
        };
    }

    public static FiltersPostcardData Map(FiltersPostcardDataRequest request)
    {
        return new FiltersPostcardData
        {
            Search = request.Search,
            City = request.City,
            Country = request.Country,
            Longitude = request.Longitude,
            Latitude = request.Latitude,
            CollectRangeInMeters = request.CollectRangeInMeters,
            Type = request.Type,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
            UserId = request.UserId,
            OrderBy = request.OrderBy
        };
    }

    public static FiltersUser Map(FiltersUserRequest request)
    {
        return new FiltersUser
        {
            Search = request.Search,
            Email = request.Email,
            NickName = request.NickName,
            CreatedFrom = request.CreatedFrom,
            CreatedTo = request.CreatedTo,
            OrderBy = request.OrderBy
        };
    }
}
