using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class UserUpdateDto : IMap
{
  public int Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public DateTime? BirthDate { get; set; }
  public string AvatarBase64 { get; set; }
  public string BackgroundBase64 { get; set; }
  public string Description { get; set; }
  public string Country { get; set; }
  public string City { get; set; }


  public void Mapping(Profile profile)
  {
    profile.CreateMap<UserUpdateDto, User>();
    profile.CreateMap<UserUpdateDto, Address>();
    profile.CreateMap<UserUpdateDto, UserDetail>();
  }
}