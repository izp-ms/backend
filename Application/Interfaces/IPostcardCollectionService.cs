using Application.Dto;

namespace Application.Interfaces;

public interface IPostcardCollectionService
{
    Task<IEnumerable<PostcardCollectionDto>> GetPostcardCollection(int userId);
}
