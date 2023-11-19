using Application.Dto;
using Application.Interfaces;
using Application.Mappings.Manual;
using Application.Requests;
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
        IEnumerable<FavouritePostcardDto> favouritePostcardDtos = FavouritePostcardMapper.Map(favouritePostcards);
        return favouritePostcardDtos;
    }

    public async Task<IEnumerable<FavouritePostcardDto>> UpdateFavouritePostcards(UpdateFavouritePostcardRequest favouritePostcardDtos)
    {
        if (favouritePostcardDtos == null)
        {
            throw new ArgumentNullException(nameof(favouritePostcardDtos));
        }

        if (!await IsPostcardIdValid(favouritePostcardDtos))
        {
            throw new ArgumentException("User doesn't have postcard with given ids");
        }

        if (!IsOrderCorrect(favouritePostcardDtos))
        {
            throw new ArgumentException("Order must be between 1 and 6 and must be unique");
        }

        IEnumerable<FavouritePostcard> favouritePostcards = await _favouritePostcardRepository.GetFavouritePostcardByUserId(favouritePostcardDtos.UserId);

        if (favouritePostcards.Any())
        {
            await _favouritePostcardRepository.DeleteRange(favouritePostcards);
        }

        IEnumerable<FavouritePostcard> mappedFavouritePostcards = FavouritePostcardMapper.Map(favouritePostcardDtos);
        await _favouritePostcardRepository.InsertRange(mappedFavouritePostcards);
        IEnumerable<FavouritePostcardDto> updatedFavouritePostcard = _mapper.Map<IEnumerable<FavouritePostcardDto>>(mappedFavouritePostcards);

        return updatedFavouritePostcard;
    }

    private async Task<bool> IsPostcardIdValid(UpdateFavouritePostcardRequest favouritePostcardDtos)
    {
        IEnumerable<UserPostcard> userPostcards = await _userPostcardRepository.GetUserPostcardByUserId(favouritePostcardDtos.UserId);

        return userPostcards.ToList().Where(postcard =>
        {
            if (!favouritePostcardDtos.PostcardIdsWithOrders.Any(data => data.PostcardId == postcard.PostcardId))
            {
                return true;
            }
            return false;
        }).Any();
    }

    private bool IsOrderCorrect(UpdateFavouritePostcardRequest favouritePostcardDtos)
    {
        if (favouritePostcardDtos.PostcardIdsWithOrders.Any(data => data.OrderId < 1 || data.OrderId > 6))
        {
            return false;
        }

        List<IGrouping<int, PostcardIdWithOrderId>> duplicates = favouritePostcardDtos.PostcardIdsWithOrders
            .GroupBy(data => data.OrderId)
            .Where(group => group.Count() > 1)
            .ToList();

        return duplicates.Count == 0;
    }
}
