using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class FavouritePostcardService : IFavouritePostcardService
{
    private readonly IFavouritePostcardRepository _favouritePostcardRepository;
    private readonly IUserPostcardRepository _userPostcardRepository;
    private readonly IMapper _mapper;

    public FavouritePostcardService(
        IFavouritePostcardRepository favouritePostcardRepository,
        IUserPostcardRepository userPostcardRepository,
        IMapper mapper)
    {
        _favouritePostcardRepository = favouritePostcardRepository;
        _userPostcardRepository = userPostcardRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FavouritePostcardDto>> GetFavouritePostcards(int userId)
    {
        IEnumerable<FavouritePostcard> favouritePostcards = await _favouritePostcardRepository.GetFavouritePostcardByUserId(userId);
        IEnumerable<FavouritePostcardDto> favouritePostcardDtos = _mapper.Map<IEnumerable<FavouritePostcardDto>>(favouritePostcards);
        return favouritePostcardDtos;
    }

    public async Task<IEnumerable<FavouritePostcardDto>> UpdateFavouritePostcards(UpdateFavouritePostcardRequest favouritePostcardDtos)
    {
        if (favouritePostcardDtos == null)
        {
            throw new ArgumentNullException(nameof(favouritePostcardDtos));
        }

        if (await IsPostcardIdValid(favouritePostcardDtos))
        {
            throw new ArgumentException("User doesn't have postcard with given ids");
        }

        IEnumerable<FavouritePostcard> favouritePostcards = await _favouritePostcardRepository.GetFavouritePostcardByUserId(favouritePostcardDtos.UserId);

        if (favouritePostcards.Any())
        {
            await _favouritePostcardRepository.DeleteRange(favouritePostcards);
        }

        IEnumerable<FavouritePostcard> mappedFavouritePostcards = _mapper.Map<IEnumerable<FavouritePostcard>>(favouritePostcardDtos);
        await _favouritePostcardRepository.InsertRange(mappedFavouritePostcards);
        IEnumerable<FavouritePostcardDto> updatedFavouritePostcard = _mapper.Map<IEnumerable<FavouritePostcardDto>>(mappedFavouritePostcards);

        return updatedFavouritePostcard;
    }

    private async Task<bool> IsPostcardIdValid(UpdateFavouritePostcardRequest favouritePostcardDtos)
    {
        IEnumerable<UserPostcard> userPostcards = await _userPostcardRepository.GetUserPostcardByUserId(favouritePostcardDtos.UserId);

        return userPostcards.ToList().Where(postcard =>
        {
            if (!favouritePostcardDtos.PostcardIds.Contains(postcard.PostcardId))
            {
                return true;
            }
            return false;
        }).Any();
    }
}
