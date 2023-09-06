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
            cfg.CreateMap<UserPostcardDto, UserPostcard>();
            cfg.CreateMap<UserPostcard, UserPostcardDto>();
            cfg.CreateMap<PostcardDto, Postcard>();
            cfg.CreateMap<Postcard, PostcardDto>()
                .ForMember(
                    dest => dest.UserId,
                    opt => opt.MapFrom(src => src.UserPostcards.FirstOrDefault().UserId)
                );
            cfg.CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.NickName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
            cfg.CreateMap<Address, UserDto>()
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.CountryCode));
            cfg.CreateMap<UserDetail, UserDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.AvatarBase64, opt => opt.MapFrom(src => src.AvatarBase64))
                .ForMember(dest => dest.BackgroundBase64, opt => opt.MapFrom(src => src.BackgroundBase64))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            cfg.CreateMap<UserStat, UserDto>()
                .ForMember(dest => dest.PostcardsSent, opt => opt.MapFrom(src => src.PostcardsSent))
                .ForMember(dest => dest.PostcardsReceived, opt => opt.MapFrom(src => src.PostcardsReceived))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score));
        })
            .CreateMapper();
    }
}