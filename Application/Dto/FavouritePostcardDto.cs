using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class FavouritePostcardDto : IMap
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostcardId { get; set; }
    public int Order { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsSent { get; set; }
    public string ImageBase64 { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string PostcardDataTitle { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public int CollectRangeInMeters { get; set; }
    public DateTime PostcardDataCreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FavouritePostcardDto, FavouritePostcard>();
        profile.CreateMap<FavouritePostcard, FavouritePostcardDto>();
    }
}

