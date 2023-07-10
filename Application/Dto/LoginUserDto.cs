using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class LoginUserDto : IMap
{
    public string Email { get; set; }
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginUserDto, User>();
    }
}
