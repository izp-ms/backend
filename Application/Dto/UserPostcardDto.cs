using Application.Mappings;
using AutoMapper;
using Domain.Entities;

public class UserPostcardDto : IMap
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostcardId { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserPostcardDto, UserPostcard>();
        profile.CreateMap<UserPostcard, UserPostcardDto>();
    }
}