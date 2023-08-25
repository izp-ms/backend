using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class AddressDto : IMap
{
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }

    public void Mapping(Profile profie)
    {
        profie.CreateMap<AddressDto, Address>();
        profie.CreateMap<Address, AddressDto>();
    }
}

