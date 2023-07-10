using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class RegisterUserDto : IMap
{
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegisterUserDto, User>();
    }
}
