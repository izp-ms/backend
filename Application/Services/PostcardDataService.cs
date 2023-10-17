using Application.Dto;
using Application.Helpers;
using Application.Interfaces;
using Application.Requests;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PostcardDataService : IPostcardDataService
{
    private readonly IPostcardDataRepository _postcardDataRepository;
    private readonly IPostcardRepository _postcardRepository;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public PostcardDataService(
        IPostcardDataRepository postcardDataRepository,
        IPostcardRepository postcardRepository,
        IUserContextService userContextService,
        IMapper mapper)
    {
        _postcardDataRepository = postcardDataRepository;
        _postcardRepository = postcardRepository;
        _userContextService = userContextService;
        _mapper = mapper;
    }

    public async Task<PostcardDataDto> AddNewPostcardData(PostcardDataDto postcardData)
    {
        if (postcardData == null)
        {
            throw new ArgumentNullException(nameof(postcardData));
        }

        PostcardData postcardDataEntity = _mapper.Map<PostcardData>(postcardData);
        PostcardData newPostcardData = await _postcardDataRepository.Insert(postcardDataEntity);
        return _mapper.Map<PostcardDataDto>(newPostcardData);
    }

    public async Task<CurrentLocationPostcardsResponse> GetPostcardsNearby(CoordinateRequest coordinateRequest)
    {
        IEnumerable<Postcard> allPostcards = await _postcardRepository.GetAll();
        IEnumerable<PostcardData> allPostcardsData = await _postcardDataRepository.GetAll();

        List<Postcard> postcards = allPostcards
            .Where(postcard => postcard.Users.Any(user => user.Id == _userContextService.GetUserId))
            .ToList();

        List<PostcardData> filteredPostcardsData = allPostcards
            .Where(postcard => postcard.Users.Any(user => user.Id != _userContextService.GetUserId))
            .Select(postcard => postcard.PostcardData)
            .Distinct()
            .ToList();

        double userLatitude = coordinateRequest.Latitude.ToDouble();
        double userLongitude = coordinateRequest.Longitude.ToDouble();

        List<PostcardDataDto> PostcardsToCollect = new List<PostcardDataDto>();
        List<PostcardDataDto> PostcardsNearby = new List<PostcardDataDto>();

        allPostcardsData.ToList().ForEach(postcard =>
        {
            double distance = Measure(postcard.Latitude.ToDouble(), postcard.Longitude.ToDouble(), userLatitude, userLongitude);

            if (distance <= postcard.CollectRangeInMeters)
            {
                PostcardsToCollect.Add(new PostcardDataDto
                {
                    Id = postcard.Id,
                    Latitude = postcard.Latitude,
                    Longitude = postcard.Longitude,
                    CollectRangeInMeters = postcard.CollectRangeInMeters,
                    ImageBase64 = postcard.ImageBase64
                });

            }
            else if (distance <= coordinateRequest.PostcardNotificationRangeInMeters)
            {
                PostcardsNearby.Add(new PostcardDataDto
                {
                    Id = postcard.Id,
                    Latitude = postcard.Latitude,
                    Longitude = postcard.Longitude,
                    CollectRangeInMeters = postcard.CollectRangeInMeters,
                    ImageBase64 = postcard.ImageBase64
                });
            }
        });

        return new CurrentLocationPostcardsResponse()
        {
            PostcardsCollected = PostcardsToCollect,
            PostcardsNearby = PostcardsNearby
        };
    }

    public async Task<PaginationResponse<PostcardDataDto>> GetPagination(PaginatedPostcardDataRequest postcardPaginationRequest)
    {
        IEnumerable<PostcardData> allPostcardsData = await GetAllPostcardsDataForPagination(postcardPaginationRequest);
        IEnumerable<PostcardData> postcardsData = await GetPostcardsDataForPagination(postcardPaginationRequest);

        int totalPages = (int)Math.Ceiling(allPostcardsData.Count() / (double)postcardPaginationRequest.PageSize);
        PaginationResponse<PostcardDataDto> paginationResponse = new PaginationResponse<PostcardDataDto>()
        {
            PageNumber = postcardPaginationRequest.PageNumber,
            PageSize = postcardPaginationRequest.PageSize,
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
            if (postcard.PostcardDataId == postcardDataId)
            {
                throw new Exception($"Postcard data with id: {postcardDataId} is used by postcard with id: {postcard.Id}");
            }
        }
        await _postcardDataRepository.Delete(postcardData);
        return _mapper.Map<PostcardDataDto>(postcardData);
    }

    private static double Measure(double postcardLatitude, double postcardLongitude, double userLatitude, double userLongitude)
    {
        double earthRad = 6378137;
        double diffrentceLatitude = (userLatitude * Math.PI / 180) - (postcardLatitude * Math.PI / 180);
        double diffrentceLongitude = (userLongitude * Math.PI / 180) - (postcardLongitude * Math.PI / 180);
        double x = Math.Sin(diffrentceLatitude / 2) * Math.Sin(diffrentceLatitude / 2) +
                   Math.Cos(postcardLatitude * Math.PI / 180) * Math.Cos(userLatitude * Math.PI / 180) *
                   Math.Sin(diffrentceLongitude / 2) * Math.Sin(diffrentceLongitude / 2);
        double distance = 2 * earthRad * Math.Atan2(Math.Sqrt(x), Math.Sqrt(1 - x));
        return distance;
    }

    private async Task<IEnumerable<PostcardData>> GetAllPostcardsDataForPagination(PaginatedPostcardDataRequest request)
    {
        if (request.UserId == null)
        {
            return await _postcardDataRepository.GetAll();
        }
        return await _postcardDataRepository.GetAllPostcardsDataByUserId((int)request.UserId);
    }

    private async Task<IEnumerable<PostcardData>> GetPostcardsDataForPagination(PaginatedPostcardDataRequest request)
    {
        if (request.UserId == null)
        {
            return await _postcardDataRepository.GetPagination(
                request.PageNumber,
                request.PageSize
            );
        }
        return await _postcardDataRepository.GetPaginationByUserId(
            request.PageNumber,
            request.PageSize,
            (int)request.UserId
        );
    }
}
