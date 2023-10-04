using System.Globalization;
using Application.Dto;
using Application.Helpers;
using Application.Interfaces;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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

    public async Task<CurrentLocationPostcardsResponse> GetPostcardsNearby(CoordinateRequest coordinateRequest)
    {
        IEnumerable<PostcardData> data = await _postcardDataRepository.GetAll();

        List<PostcardData> list = data.ToList();//.Where(data.isSent == false);


        double userLati = coordinateRequest.Latitude.ToDouble();
        double userLong = coordinateRequest.Longitude.ToDouble();

        List<PostcardDataDto> PostcardsToCollect = new List<PostcardDataDto>();
        List<PostcardDataDto> PostcardsNearby = new List<PostcardDataDto>();

        list.ForEach(item =>
        {
            //double.ToDouble(item.Latitude)
            double distance = Measure(item.Latitude.ToDouble(), item.Longitude.ToDouble(), userLati, userLong);

            if (distance <= item.CollectRangeInMeters)
            {
                PostcardsToCollect.Add(new PostcardDataDto
                {
                    Id = item.Id,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    CollectRangeInMeters = item.CollectRangeInMeters,
                    ImageBase64 = item.ImageBase64
                });

            }
            else if (distance <= 5000)
            {
                PostcardsNearby.Add(new PostcardDataDto
                {
                    Id = item.Id,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    CollectRangeInMeters = item.CollectRangeInMeters,
                    ImageBase64 = item.ImageBase64
                });
            }
        });
        return new CurrentLocationPostcardsResponse()
        {
            PostcardsCollected = PostcardsToCollect,
            PostcardsNearby = PostcardsNearby
        };
    }

    private double Measure(object value, double v, double userLati, double userLong)
    {
        throw new NotImplementedException();
    }

    private double Measure(double postcardLatitude, double postcardLongitude, double userLatitude, double userLongitude)
    {
        double earthRad = 6378137; // meters
        double diffrentceLatitude = (userLatitude * Math.PI / 180) - (postcardLatitude * Math.PI / 180);
        double diffrentceLongitude = (userLongitude * Math.PI / 180) - (postcardLongitude * Math.PI / 180);
        double a = Math.Sin(diffrentceLatitude / 2) * Math.Sin(diffrentceLatitude / 2) +
                   Math.Cos(postcardLatitude * Math.PI / 180) * Math.Cos(userLatitude * Math.PI / 180) *
                   Math.Sin(diffrentceLongitude / 2) * Math.Sin(diffrentceLongitude / 2);
        double distance = 2 * earthRad * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return distance;
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
            if (postcard.PostcardDataId == postcardDataId)
            {
                throw new Exception($"Postcard data with id: {postcardDataId} is used by postcard with id: {postcard.Id}");
            }
        }
        await _postcardDataRepository.Delete(postcardData);
        return _mapper.Map<PostcardDataDto>(postcardData);
    }
}
