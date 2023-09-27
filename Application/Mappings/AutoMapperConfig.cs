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
            cfg.CreateMap<PostcardDataDto, PostcardData>();
            cfg.CreateMap<PostcardData, PostcardDataDto>();
            cfg.CreateMap<UserPostcardDto, UserPostcard>();
            cfg.CreateMap<UserPostcard, UserPostcardDto>();
            cfg.CreateMap<PostcardDto, Postcard>();

            cfg.CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.NickName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
            cfg.CreateMap<Address, UserDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
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

            cfg.CreateMap<UserUpdateDto, User>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UsersDetails.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.UsersDetails.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UsersDetails.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.UsersDetails.AvatarBase64, opt => opt.MapFrom(src => src.AvatarBase64))
                .ForMember(dest => dest.UsersDetails.BackgroundBase64, opt => opt.MapFrom(src => src.BackgroundBase64))
                .ForMember(dest => dest.UsersDetails.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Address.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Address.Country, opt => opt.MapFrom(src => src.Country));

            cfg.CreateMap<User, FriendDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.NickName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
            cfg.CreateMap<Address, FriendDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
            cfg.CreateMap<UserDetail, FriendDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.AvatarBase64, opt => opt.MapFrom(src => src.AvatarBase64))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            cfg.CreateMap<UserStat, FriendDto>()
                .ForMember(dest => dest.PostcardsSent, opt => opt.MapFrom(src => src.PostcardsSent))
                .ForMember(dest => dest.PostcardsReceived, opt => opt.MapFrom(src => src.PostcardsReceived))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score));

            cfg.CreateMap<UserFriends, FriendDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FriendId))
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.Friend.NickName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Friend.Email))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Friend.CreatedAt))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Friend.UsersDetails.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Friend.UsersDetails.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.Friend.UsersDetails.BirthDate))
                .ForMember(dest => dest.AvatarBase64, opt => opt.MapFrom(src => src.Friend.UsersDetails.AvatarBase64))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Friend.UsersDetails.Description))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Friend.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Friend.Address.Country))
                .ForMember(dest => dest.PostcardsSent, opt => opt.MapFrom(src => src.Friend.UsersStats.PostcardsSent))
                .ForMember(dest => dest.PostcardsReceived, opt => opt.MapFrom(src => src.Friend.UsersStats.PostcardsReceived))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Friend.UsersStats.Score));

            cfg.CreateMap<PostcardCollection, PostcardCollectionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.PostcardData, opt => opt.MapFrom(src => src.PostcardData));
        })
            .CreateMapper();
    }
}