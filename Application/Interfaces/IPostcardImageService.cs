using Application.Dto;
using Application.Response;

namespace Application.Interfaces;

public interface IPostcardImageService
{
    Task<PostcardImageDto> AddNewPostcardImage(PostcardImageDto postcardImage);
    Task<PaginationResponse<PostcardImageDto>> GetPagination(PaginationRequest paginationRequest);
    Task<PostcardImageDto> DeletePostcardImage(int postcardImageId);
}
