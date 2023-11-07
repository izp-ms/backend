using Application.Dto;
using Application.Requests;
using Application.Response;

namespace Application.Interfaces;

public interface IPostcardService
{
    Task<PostcardDto> AddNewPostcard(PostcardDto postcardDto);
    Task<PaginationResponse<PostcardWithDataDto>> GetPagination(PaginationRequest pagination, FiltersPostcardRequest filters);
    Task<PostcardDto> GetPostcardById(int postcardId);
    Task<PostcardDto> UpdatePostcard(PostcardDto postcardDto);
    Task<UserPostcardDto> TransferPostcard(int postcardId, int userId);
}
