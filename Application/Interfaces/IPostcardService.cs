using Application.Dto;
using Application.Response;

namespace Application.Interfaces;

public interface IPostcardService
{
    Task<PostcardDto> AddNewPostcard(PostcardDto postcardDto);
    Task<PaginationResponse<PostcardWithDataDto>> GetPagination(PostcardPaginationRequest postcardPaginationRequest);
    Task<PostcardDto> GetPostcardById(int postcardId);
    Task<PostcardDto> UpdatePostcard(PostcardDto postcardDto);
    Task<UserPostcardDto> TransferPostcard(int postcardId, int userId);
    Task<PostcardDto> DeletePostcard(int postcardId);
}
