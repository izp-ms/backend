using Application.Dto;
using Application.Interfaces;
using Application.Mappings.Manual;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PostcardCollectionService : IPostcardCollectionService
{
    private readonly IPostcardCollectionRepository _postcardCollectionRepository;

    public PostcardCollectionService(IPostcardCollectionRepository postcardCollectionRepository)
    {
        _postcardCollectionRepository = postcardCollectionRepository;
    }

    public async Task<PostcardCollectionDto> GetPostcardCollection(int userId)
    {
        IEnumerable<PostcardCollection> postcardCollection = await _postcardCollectionRepository.GetPostcardCollectionByUserId(userId);
        return PostcardCollectionMapper.Map(postcardCollection);
    }
}
