using Application.Dto;
using Application.Requests;

namespace Application.Helpers;

public static class CacheKeyGenerator
{
    public static string GetKey(int? userId, PaginationRequest pagination, FiltersPostcardDataRequest filters)
    {
        return $"{userId}_{pagination.PageNumber}_{pagination.PageSize}_{filters.Search}_{filters.City}_{filters.Country}_{filters.Longitude}_{filters.Latitude}_{filters.CollectRangeInMeters}_{filters.DateFrom}_{filters.DateTo}_{filters.UserId}_{filters.OrderBy}";
    }

    public static string GetKey(int? userId, PaginationRequest pagination, FiltersUserRequest filters)
    {
        return $"{userId}_{pagination.PageNumber}_{pagination.PageSize}_{filters.Search}_{filters.Email}_{filters.NickName}_{filters.CreatedFrom}_{filters.CreatedTo}_{filters.OrderBy}";
    }

    public static string GetKey(int? userId, CoordinateRequest coordinateRequest)
    {
        return $"{userId}_{coordinateRequest.Latitude}_{coordinateRequest.Longitude}_{coordinateRequest.PostcardNotificationRangeInMeters}";
    }

    public static string GetKey(int? userId, PaginationRequest pagination, FiltersPostcardRequest filters)
    {
        return $"{userId}_{pagination.PageNumber}_{pagination.PageSize}_{filters.Search}_{filters.Type}_{filters.IsSent}_{filters.UserId}_{filters.DateFrom}_{filters.DateTo}_{filters.OrderBy}";
    }

    public static string GetKey(int userId, int? contextUserId)
    {
        return $"{userId}_{contextUserId}";
    }

    public static string GetKey(int? userId, UserUpdateDto userUpdateDto)
    {
        return $"{userId}_{userUpdateDto.FirstName}_{userUpdateDto.LastName}_{userUpdateDto.City}_{userUpdateDto.Country}";
    }
}
