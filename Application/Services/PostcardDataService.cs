using Application.Dto;
using Application.Interfaces;
using Application.Response;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PostcardDataService : IPostcardDataService
{
    private readonly IPostcardDataRepository _postcardDataRepository;
    private readonly IPostcardRepository _postcardRepository;
    private readonly IMapper _mapper;

    public PostcardDataService(IPostcardDataRepository postcardDataRepository, IPostcardRepository postcardRepository, IMapper mapper)
    {
        _postcardDataRepository = postcardDataRepository;
        _postcardRepository = postcardRepository;
        _mapper = mapper;
    }

    public async Task<PostcardDataDto> AddNewPostcardData(PostcardDataDto postcardData)
    {
        if (postcardData == null)
        {
            throw new ArgumentNullException(nameof(postcardData));
        }

        // if (!ImageValidator.IsImageValid(postcardData.ImageBase64))
        // {
        //     throw new Exception("Image has wrong aspect ratio");
        // }

        PostcardData postcardDataEntity = _mapper.Map<PostcardData>(postcardData);
        PostcardData newPostcardData = await _postcardDataRepository.Insert(postcardDataEntity);
        return _mapper.Map<PostcardDataDto>(newPostcardData);
    }

    public async Task<PaginationResponse<PostcardDataDto>> GetPagination(PaginationRequest paginationRequest)
    {
        IEnumerable<PostcardData> allPostcardsData = await _postcardDataRepository.GetAll();
        IEnumerable<PostcardData> postcardsData = await _postcardDataRepository.GetPagination(paginationRequest.PageNumber, paginationRequest.PageSize);
        int totalPages = (int)Math.Ceiling(allPostcardsData.Count() / (double)paginationRequest.PageSize);
        PaginationResponse<PostcardDataDto> paginationResponse = new PaginationResponse<PostcardDataDto>()
        {
            PageNumber = paginationRequest.PageNumber,
            PageSize = paginationRequest.PageSize,
            TotalCount = allPostcardsData.Count(),
            TotalPages = totalPages,
            Content = _mapper.Map<IEnumerable<PostcardData>, IEnumerable<PostcardDataDto>>(postcardsData)
        };
        return paginationResponse;
    }

    public async Task<PostcardDataDto> DeletePostcardData(int postcardDataId)
    {
        PostcardData postcardData = await _postcardDataRepository.Get(postcardDataId);
        if (postcardData == null)
        {
            throw new Exception($"Postcard data with id: {postcardDataId} does not exist");
        }
        IEnumerable<Postcard> postcards = await _postcardRepository.GetAll();
        foreach (Postcard postcard in postcards)
        {
            if (postcard.ImageId == postcardDataId)
            {
                throw new Exception($"Postcard data with id: {postcardDataId} is used by postcard with id: {postcard.Id}");
            }
        }
        await _postcardDataRepository.Delete(postcardData);
        return _mapper.Map<PostcardDataDto>(postcardData);
    }
}
