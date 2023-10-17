using Application.Dto;
using Application.Requests;
using Application.Response;

namespace Application.Interfaces;

public interface IPostcardDataService
{
    Task<PostcardDataDto> AddNewPostcardData(PostcardDataDto postcardData);
    Task<PaginationResponse<PostcardDataDto>> GetPagination(PaginatedPostcardDataRequest postcardPaginationRequest);
    Task<CurrentLocationPostcardsResponse> GetPostcardsNearby(CoordinateRequest coordinateRequest);
    Task<PostcardDataDto> DeletePostcardData(int postcardDataId);
}
