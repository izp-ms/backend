using Application.Dto;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPostcardImageService
{
    Task<PostcardImageDto> AddNewPostcardImage(PostcardImageDto postcardImage);
    Task<PaginationResponse<PostcardImageDto>> GetPagination(PaginationRequest paginationRequest);
}
