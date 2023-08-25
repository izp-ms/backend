using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class UserDetailDto : IMap
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string AvatarBase64 { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profie)
    {
        profie.CreateMap<UserDetailDto, UserDetail>();
        profie.CreateMap<UserDetail, UserDetailDto>();
    }
}
