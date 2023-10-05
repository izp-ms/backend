using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class UpdateFavouritePostcardRequest : IMap
{
    public int UserId { get; set; }
    public IEnumerable<int> PostcardIds { get; set; } = new List<int>();
    public IEnumerable<int> Orders { get; set; } = new List<int>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateFavouritePostcardRequest, FavouritePostcard>();
    }
}

