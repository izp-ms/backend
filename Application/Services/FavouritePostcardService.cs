using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class FavouritePostcardService : IFavouritePostcardService
{
    private readonly IFavouritePostcardRepository _favouritePostcardRepository;
    private readonly IMapper _mapper;

    public FavouritePostcardService(IFavouritePostcardRepository favouritePostcardRepository, IMapper mapper)
    {
        _favouritePostcardRepository = favouritePostcardRepository;
        _mapper = mapper;
    }

}
