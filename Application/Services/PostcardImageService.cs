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
    private readonly IPostcardRepository _postcardRepository;
    private readonly IMapper _mapper;

    public PostcardImageService(IPostcardImageRepository postcardImageRepository, IPostcardRepository postcardRepository, IMapper mapper)
    {
        _postcardImageRepository = postcardImageRepository;
        _postcardRepository = postcardRepository;
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

    public async Task<PostcardImageDto> DeletePostcardImage(int postcardImageId)
    {
        PostcardImage postcardImage = await _postcardImageRepository.Get(postcardImageId);
        if (postcardImage == null)
        {
            throw new Exception($"Postcard image with id: {postcardImageId} does not exist");
        }
        IEnumerable<Postcard> postcards = await _postcardRepository.GetAll();
        foreach (Postcard postcard in postcards)
        {
            if (postcard.ImageId == postcardImageId)
            {
                throw new Exception($"Postcard image with id: {postcardImageId} is used by postcard with id: {postcard.Id}");
            }
        }
        await _postcardImageRepository.Delete(postcardImage);
        return _mapper.Map<PostcardImageDto>(postcardImage);
    }
}
