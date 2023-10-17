using Application.Dto;
using Application.Requests;
using Application.Response;

namespace Application.Interfaces;

public interface IPostcardDataService
{
    Task<PostcardDataDto> AddNewPostcardData(PostcardDataDto postcardData);
    Task<PaginationResponse<PostcardDataDto>> GetPagination(PaginationRequest pagination, FiltersPostcardDataRequest filters);
    Task<CurrentLocationPostcardsResponse> GetPostcardsNearby(CoordinateRequest coordinateRequest);
    Task<PostcardDataDto> DeletePostcardData(int postcardDataId);
}
