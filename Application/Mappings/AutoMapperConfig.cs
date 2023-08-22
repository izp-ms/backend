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
        })
            .CreateMapper();
    }

}