using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class PostcardCollectionDto : IMap
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public IEnumerable<PostcardData> PostcardData { get; set; } = new List<PostcardData>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PostcardCollection, PostcardCollectionDto>();
    }
}
