using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public AddressService(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public async Task<AddressDto> GetAddress(int userId)
    {
        Address address = await _addressRepository.Get(userId) ?? throw new Exception(userId.ToString()); ;
        return _mapper.Map<AddressDto>(address);
    }

    public async Task<AddressDto> UpdateAddress(AddressDto addressDto)
    {
        Address mappedAddress = _mapper.Map<Address>(addressDto);
        if (mappedAddress == null)
        {
            throw new Exception($"Address with id {addressDto.Id} not found");
        }
        Address address = await _addressRepository.Update(mappedAddress) ?? throw new Exception("Address not updated");
        return _mapper.Map<AddressDto>(address);
    }
}
