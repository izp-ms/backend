using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class AddressDto : IMap
{
    public int Id { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddressDto, Address>();
        profile.CreateMap<Address, AddressDto>();
    }
}

