using Application.Dto;
using Application.Interfaces;
using Application.Mappings;
using Application.Requests;
using Application.Response;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PostcardService : IPostcardService
{
    private readonly IPostcardRepository _postcardRepository;
    private readonly IUserPostcardRepository _userPostcardRepository;
    private readonly IUserContextService _userContextService;
    private readonly IUserStatsService _userStatsService;
    private readonly IMapper _mapper;

    public PostcardService(
        IUserPostcardRepository userPostcardRepository,
        IPostcardRepository postcardRepository,
        IUserContextService userContextService,
        IUserStatsService userStatsService,
        IMapper mapper)
    {
        _userPostcardRepository = userPostcardRepository;
        _postcardRepository = postcardRepository;
        _userContextService = userContextService;
        _userStatsService = userStatsService;
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
            ReceivedAt = DateTime.UtcNow,
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

    public async Task<PaginationResponse<PostcardWithDataDto>> GetPagination(PaginatedPostcardDataRequest request)
    {
        if (request.UserId == null || _userContextService.GetUserId == null)
        {
            throw new Exception("User not found");
        }

        IEnumerable<Postcard> allPostcards = await _postcardRepository.GetAllPostcardsByUserId((int)request.UserId);
        IEnumerable<Postcard> postcards = await _postcardRepository.GetPaginationByUserId(
            request.PageNumber,
            request.PageSize,
            (int)request.UserId);

        IEnumerable<PostcardWithDataDto> mappedPostcards = PostcardWithDataDtoMapper.Map(postcards, (int)request.UserId);

        int totalPages = (int)Math.Ceiling(allPostcards.Count() / (double)request.PageSize);

        PaginationResponse<PostcardWithDataDto> paginationResponse = new PaginationResponse<PostcardWithDataDto>()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = allPostcards.Count(),
            TotalPages = totalPages,
            Content = mappedPostcards
        };

        return paginationResponse;
    }

    public async Task<PostcardDto> GetPostcardById(int postcardId)
    {
        Postcard postcard = await _postcardRepository.Get(postcardId) ?? throw new Exception("Postcard not found");
        return _mapper.Map<PostcardDto>(postcard);
    }

    public async Task<UserPostcardDto> TransferPostcard(int postcardId, int newUserId)
    {
        UserPostcard userPostcard = await _userPostcardRepository.GetUserPostcardByPostcardId(postcardId);
        if (!PostcardTransferValidator.IsPostcardValid(userPostcard, newUserId, _userContextService.GetUserId))
        {
            throw new Exception("Postcard is not valid");
        }

        PostcardDto postcard = await GetPostcardById(postcardId);
        UserStatDto sender = await _userStatsService.GetUserStatsById(_userContextService.GetUserId ?? userPostcard.UserId);
        UserStatDto receiver = await _userStatsService.GetUserStatsById(newUserId);

        if (postcard.IsSent)
        {
            throw new Exception("Postcard already received by user");
        }

        if (!PostcardTransferValidator.IsSenderAndReceiverValid(sender, receiver))
        {
            throw new Exception("User not found");
        }

        sender.PostcardsSent++;
        sender.Score++;
        receiver.PostcardsReceived++;
        receiver.Score++;
        postcard.IsSent = true;

        await UpdatePostcard(postcard);
        await _userStatsService.UpdateUserStats(sender);
        await _userStatsService.UpdateUserStats(receiver);

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