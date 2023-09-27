using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class PostcardDataDto : IMap
{
    public int Id { get; set; }
    public string ImageBase64 { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Title { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public int CollectRangeInMeters { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PostcardDataDto, PostcardData>();
        profile.CreateMap<PostcardData, PostcardDataDto>();
    }
}
