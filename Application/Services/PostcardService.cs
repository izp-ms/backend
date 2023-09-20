using Application.Dto;
using Application.Interfaces;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PostcardService : IPostcardService
{
    private readonly IPostcardRepository _postcardRepository;
    private readonly IUserPostcardRepository _userPostcardRepository;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public PostcardService(
        IUserPostcardRepository userPostcardRepository,
        IPostcardRepository postcardRepository,
        IUserContextService userContextService,
        IMapper mapper)
    {
        _userPostcardRepository = userPostcardRepository;
        _postcardRepository = postcardRepository;
        _userContextService = userContextService;
        _mapper = mapper;
    }

    public async Task<PostcardDto> AddNewPostcard(PostcardDto postcardDto)
    {
        if (postcardDto == null)
        {
            throw new ArgumentNullException(nameof(postcardDto));
        }

        Postcard postcardEntity = _mapper.Map<Postcard>(postcardDto);
        Postcard newPostcard = await _postcardRepository.Insert(postcardEntity);
        UserPostcard userPostcard = new UserPostcard()
        {
            UserId = postcardDto.UserId,
            PostcardId = newPostcard.Id,
            CreatedAt = DateTime.UtcNow,
        };
        await _userPostcardRepository.Insert(userPostcard);
        return _mapper.Map<PostcardDto>(newPostcard);
    }

    public async Task<PostcardDto> DeletePostcard(int postcardId)
    {
        Postcard postcard = await _postcardRepository.Get(postcardId);
        if (postcard == null)
        {
            throw new Exception("Postcard not found");
        }

        await _postcardRepository.Delete(postcard);
        return _mapper.Map<PostcardDto>(postcard);
    }

    public async Task<PaginationResponse<PostcardDto>> GetPagination(PaginationRequest paginationRequest)
    {
        IEnumerable<Postcard> allPostcards = await _postcardRepository.GetAll();
        IEnumerable<Postcard> postcards = await _postcardRepository.GetPagination(paginationRequest.PageNumber, paginationRequest.PageSize);
        int totalPages = (int)Math.Ceiling(allPostcards.Count() / (double)paginationRequest.PageSize);
        PaginationResponse<PostcardDto> paginationResponse = new PaginationResponse<PostcardDto>()
        {
            PageNumber = paginationRequest.PageNumber,
            PageSize = paginationRequest.PageSize,
            TotalCount = allPostcards.Count(),
            TotalPages = totalPages,
            Content = _mapper.Map<IEnumerable<Postcard>, IEnumerable<PostcardDto>>(postcards)
        };

        return paginationResponse;
    }

    public async Task<PostcardDto> GetPostcardById(int postcardId)
    {
        Postcard postcard = await _postcardRepository.Get(postcardId);
        if (postcard == null)
        {
            throw new Exception("Postcard not found");
        }

        return _mapper.Map<PostcardDto>(postcard);
    }

    public async Task<UserPostcardDto> TransferPostcard(int postcardId, int newUserId)
    {
        UserPostcard userPostcard = await _userPostcardRepository.GetUserPostcardByPostcardId(postcardId);
        if (userPostcard == null)
        {
            throw new Exception("Postcard not found");
        }
        userPostcard.UserId = newUserId;
        UserPostcard updatedUserPostcard = await _userPostcardRepository.Update(userPostcard);
        return _mapper.Map<UserPostcardDto>(updatedUserPostcard);
    }

    public async Task<PostcardDto> UpdatePostcard(PostcardDto postcardDto)
    {
        if (postcardDto == null)
        {
            throw new ArgumentNullException(nameof(postcardDto));
        }

        Postcard postcardEntity = _mapper.Map<Postcard>(postcardDto);
        Postcard updatedPostcard = await _postcardRepository.Update(postcardEntity);
        return _mapper.Map<PostcardDto>(updatedPostcard);
    }
}