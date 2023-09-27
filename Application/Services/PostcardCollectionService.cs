using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PostcardCollectionService : IPostcardCollectionService
{
    private readonly IPostcardCollectionRepository _postcardCollectionRepository;
    private readonly IMapper _mapper;

    public PostcardCollectionService(IPostcardCollectionRepository postcardCollectionRepository, IMapper mapper)
    {
        _postcardCollectionRepository = postcardCollectionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PostcardCollectionDto>> GetPostcardCollection(int userId)
    {
        IEnumerable<PostcardCollection> postcardCollection = await _postcardCollectionRepository.GetPostcardCollectionByUserId(userId);
        return _mapper.Map<IEnumerable<PostcardCollectionDto>>(postcardCollection);
    }
}
