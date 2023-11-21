using Application.Mappings;
using AutoMapper;
using Domain.Entities;

public class PostcardDto : IMap
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int PostcardDataId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public bool IsSent { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PostcardDto, Postcard>();
        profile.CreateMap<Postcard, PostcardDto>();
    }
}