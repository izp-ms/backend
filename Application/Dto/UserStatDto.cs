using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class UserStatDto : IMap
{
    public int Id { get; set; }
    public int PostcardsSent { get; set; }
    public int PostcardsReceived { get; set; }
    public int Score { get; set; }

    public void Mapping(Profile profie)
    {
        profie.CreateMap<UserStatDto, UserStat>();
        profie.CreateMap<UserStat, UserStatDto>();
    }
}
