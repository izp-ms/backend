using Application.Mappings;
using AutoMapper;
using Domain.Entities;

public class PostcardDto : IMap
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int ImageId { get; set; }
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PostcardDto, Postcard>();
        profile.CreateMap<Postcard, PostcardDto>();
    }
}