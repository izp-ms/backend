using Application.Dto;
using Application.Interfaces;
using Application.Response;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PostcardImageService : IPostcardImageService
{
    private readonly IPostcardImageRepository _postcardImageRepository;
    private readonly IMapper _mapper;

    public PostcardImageService(IPostcardImageRepository postcardImageRepository, IMapper mapper)
    {
        _postcardImageRepository = postcardImageRepository;
        _mapper = mapper;
    }

    public async Task<PostcardImageDto> AddNewPostcardImage(PostcardImageDto postcardImage)
    {
        if (postcardImage == null)
        {
            throw new ArgumentNullException(nameof(postcardImage));
        }

        if (!ImageValidator.IsImageValid(postcardImage.ImageBase64))
        {
            throw new Exception("Image has wrong aspect ratio");
        }

        PostcardImage postcardImageEntity = _mapper.Map<PostcardImage>(postcardImage);
        PostcardImage newPostcardImage = await _postcardImageRepository.Insert(postcardImageEntity);
        return _mapper.Map<PostcardImageDto>(newPostcardImage);
    }

    public async Task<PaginationResponse<PostcardImageDto>> GetPagination(PaginationRequest paginationRequest)
    {
        IEnumerable<PostcardImage> allPostcardImages = await _postcardImageRepository.GetAll();
        IEnumerable<PostcardImage> postcardImages = await _postcardImageRepository.GetPagination(paginationRequest.PageNumber, paginationRequest.PageSize);
        int totalPages = (int)Math.Ceiling(allPostcardImages.Count() / (double)paginationRequest.PageSize);
        PaginationResponse<PostcardImageDto> paginationResponse = new PaginationResponse<PostcardImageDto>()
        {
            PageNumber = paginationRequest.PageNumber,
            PageSize = paginationRequest.PageSize,
            TotalCount = allPostcardImages.Count(),
            TotalPages = totalPages,
            Content = _mapper.Map<IEnumerable<PostcardImage>, IEnumerable<PostcardImageDto>>(postcardImages)
        };
        return paginationResponse;
    }
}
