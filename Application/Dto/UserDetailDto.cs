using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class UserDetailDto : IMap
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string BackgroundBase64 { get; set; }
    public string AvatarBase64 { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserDetailDto, UserDetail>();
        profile.CreateMap<UserDetail, UserDetailDto>();
    }
}
