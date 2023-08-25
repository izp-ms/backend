using Application.Dto;
using Application.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public static class AutoMapperConfig
{
    public static IMapper Initialize()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<RegisterUserDto, User>();
            cfg.CreateMap<LoginUserDto, User>();
            cfg.CreateMap<User, RegisterResponse>();
            cfg.CreateMap<UserStatDto, UserStat>();
            cfg.CreateMap<UserStat, UserStatDto>();
            cfg.CreateMap<AddressDto, Address>();
            cfg.CreateMap<Address, AddressDto>();
            cfg.CreateMap<UserDetailDto, UserDetail>();
            cfg.CreateMap<UserDetail, UserDetailDto>();
            cfg.CreateMap<PostcardImageDto, PostcardImage>();
            cfg.CreateMap<PostcardImage, PostcardImageDto>();
        })
            .CreateMapper();
    }
}