using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class UserDto : IMap
{
    public int Id { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string AvatarBase64 { get; set; }
    public string BackgroundBase64 { get; set; }
    public string Description { get; set; }

    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }

    public int PostcardsSent { get; set; }
    public int PostcardsReceived { get; set; }
    public int Score { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>();
        profile.CreateMap<Address, UserDto>();
        profile.CreateMap<UserDetail, UserDto>();
        profile.CreateMap<UserStat, UserDto>();
    }
}