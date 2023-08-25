using Application.Dto;

namespace Application.Interfaces;

public interface IAddressService
{
    Task<AddressDto> GetAddress(int userId);
    Task<AddressDto> UpdateAddress(AddressDto addressDto);
}
