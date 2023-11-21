using Application.Dto;
using Application.Requests;
using Domain.Entities;

namespace Application.Mappings.Manual;

public static class FavouritePostcardMapper
{
    public static IEnumerable<FavouritePostcard> Map(UpdateFavouritePostcardRequest updateFavouritePostcardRequest)
    {
        return updateFavouritePostcardRequest.PostcardIdsWithOrders.Select(data =>
        {
            return new FavouritePostcard()
            {
                PostcardId = data.PostcardId,
                Order = data.OrderId,
                UserId = updateFavouritePostcardRequest.UserId
            };
        });
    }

    public static IEnumerable<FavouritePostcardDto> Map(IEnumerable<FavouritePostcard> favouritePostcards)
    {
        return favouritePostcards.Select(favouritePostcard =>
        {
            return new FavouritePostcardDto()
            {
                Id = favouritePostcard.Id,
                UserId = favouritePostcard.UserId,
                PostcardId = favouritePostcard.PostcardId,
                Order = favouritePostcard.Order,
                Title = favouritePostcard.Postcard.Title,
                Content = favouritePostcard.Postcard.Content,
                CreatedAt = favouritePostcard.Postcard.CreatedAt,
                IsSent = favouritePostcard.Postcard.IsSent,
                ImageBase64 = favouritePostcard.Postcard.PostcardData.ImageBase64,
                Country = favouritePostcard.Postcard.PostcardData.Country,
                City = favouritePostcard.Postcard.PostcardData.City,
                PostcardDataTitle = favouritePostcard.Postcard.PostcardData.Title,
                Longitude = favouritePostcard.Postcard.PostcardData.Longitude,
                Latitude = favouritePostcard.Postcard.PostcardData.Latitude,
                CollectRangeInMeters = favouritePostcard.Postcard.PostcardData.CollectRangeInMeters,
                Type = favouritePostcard.Postcard.PostcardData.Type,
                PostcardDataCreatedAt = favouritePostcard.Postcard.PostcardData.CreatedAt
            };
        });
    }
}
