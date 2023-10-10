using Application.Dto;
using Application.Response;

namespace Application.Interfaces;

public interface IPostcardDataService
{
    Task<PostcardDataDto> AddNewPostcardData(PostcardDataDto postcardData);
    Task<PaginationResponse<PostcardDataDto>> GetPagination(PostcardPaginationRequest postcardPaginationRequest);
    Task<CurrentLocationPostcardsResponse> GetPostcardsNearby(CoordinateRequest coordinateRequest);
    Task<PostcardDataDto> DeletePostcardData(int postcardDataId);
}
