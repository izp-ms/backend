using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Response;

public class RegisterResponse : IMap
{
    public int Id { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, RegisterResponse>();
    }
}
