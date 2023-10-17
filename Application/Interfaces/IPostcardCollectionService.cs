using Application.Dto;

namespace Application.Interfaces;

public interface IPostcardCollectionService
{
    Task<PostcardCollectionDto> GetPostcardCollection(int userId);
}
