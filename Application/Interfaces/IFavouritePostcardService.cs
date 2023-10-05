using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IFavouritePostcardService
{
    Task<IEnumerable<FavouritePostcardDto>> GetFavouritePostcards(int userId);
    Task<IEnumerable<FavouritePostcardDto>> UpdateFavouritePostcards(UpdateFavouritePostcardRequest favouritePostcardDtos);
}
