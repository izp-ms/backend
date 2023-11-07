using Application.Dto;
using Application.Helpers;
using Application.Interfaces;
using Application.Mappings.Manual;
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
    private readonly IPostcardCollectionRepository _postcardCollectionRepository;
    private readonly IUserPostcardRepository _userPostcardRepository;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public PostcardDataService(
        IPostcardDataRepository postcardDataRepository,
        IPostcardRepository postcardRepository,
        IPostcardCollectionRepository postcardCollectionRepository,
        IUserPostcardRepository userPostcardRepository,
        IUserContextService userContextService,
        IMapper mapper)
    {
        _postcardDataRepository = postcardDataRepository;
        _postcardRepository = postcardRepository;
        _postcardCollectionRepository = postcardCollectionRepository;
        _userPostcardRepository = userPostcardRepository;
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

    public async Task<PostcardDto> CollectPostcardData(int userId, int postcardDataId, CoordinateRequest coordinateRequest)
    {
        if (postcardDataId == 0)
        {
            throw new ArgumentNullException(nameof(postcardDataId));
        }

        if (!await ValidateUserCoordinationPostcardLocations(coordinateRequest, postcardDataId))
        {
            throw new Exception("User is not in range of this postcard or postcard does not exist");
        }

        if (await CheckIfUserAlreadyCollectedPostcard(userId, postcardDataId))
        {
            throw new Exception("User already collected this postcard");
        }

        await _postcardCollectionRepository.Insert(new PostcardCollection()
        {
            UserId = userId,
            PostcardDataId = postcardDataId,
        });

        Postcard postcard = await _postcardRepository.Insert(new Postcard()
        {
            Title = "",
            Content = "",
            PostcardDataId = postcardDataId,
            Type = "PLACE",
            CreatedAt = DateTime.UtcNow,
            IsSent = false,
        });

        await _userPostcardRepository.Insert(new UserPostcard()
        {
            UserId = userId,
            PostcardId = postcard.Id,
            ReceivedAt = DateTime.UtcNow,
        });

        return _mapper.Map<PostcardDto>(postcard);
    }

    public async Task<PostcardDataDto> UpdatePostcardData(PostcardDataDto postcardData)
    {
        if (postcardData == null)
        {
            throw new ArgumentNullException(nameof(postcardData));
        }

        PostcardData postcardDataEntity = _mapper.Map<PostcardData>(postcardData);
        PostcardData newPostcardData = await _postcardDataRepository.Update(postcardDataEntity);
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

    public async Task<PaginationResponse<PostcardDataDto>> GetPagination(
        PaginationRequest request,
        FiltersPostcardDataRequest filters
    )
    {
        IEnumerable<PostcardData> allPostcardsData = await _postcardDataRepository.GetAllPostcardsData(FiltersMapper.Map(filters));
        IEnumerable<PostcardData> postcardsData = await _postcardDataRepository.GetPagination(PaginationMapper.Map(request), FiltersMapper.Map(filters));

        int totalPages = (int)Math.Ceiling(allPostcardsData.Count() / (double)request.PageSize);
        PaginationResponse<PostcardDataDto> paginationResponse = new PaginationResponse<PostcardDataDto>()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = allPostcardsData.Count(),
            TotalPages = totalPages,
            Content = _mapper.Map<IEnumerable<PostcardData>, IEnumerable<PostcardDataDto>>(postcardsData)
        };
        return paginationResponse;
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

    private async Task<bool> ValidateUserCoordinationPostcardLocations(CoordinateRequest coordinateRequest, int postcardDataId)
    {
        IEnumerable<PostcardData> allPostcardsData = await _postcardDataRepository.GetAll();
        List<PostcardDataDto> PostcardsToCollect = new List<PostcardDataDto>();
        double userLatitude = coordinateRequest.Latitude.ToDouble();
        double userLongitude = coordinateRequest.Longitude.ToDouble();
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
        });

        bool isPostcardDataIdInPostcardsToCollect = PostcardsToCollect.Any(postcard => postcard.Id == postcardDataId);
        return isPostcardDataIdInPostcardsToCollect;
    }

    private async Task<bool> CheckIfUserAlreadyCollectedPostcard(int userId, int postcardDataId)
    {
        IEnumerable<PostcardCollection> postcardCollections = await _postcardCollectionRepository.GetAll();
        bool isUserAlreadyCollectedPostcard = postcardCollections.Any(postcardCollection => postcardCollection.UserId == userId && postcardCollection.PostcardDataId == postcardDataId);
        return isUserAlreadyCollectedPostcard;
    }
}
