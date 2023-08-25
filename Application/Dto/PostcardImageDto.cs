using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class PostcardImageDto : IMap
{
    public int Id { get; set; }
    public string ImageBase64 { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PostcardImageDto, PostcardImage>();
        profile.CreateMap<PostcardImage, PostcardImageDto>();
    }
}
