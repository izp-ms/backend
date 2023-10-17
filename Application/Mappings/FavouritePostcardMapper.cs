using Application.Requests;
using Domain.Entities;

namespace Application.Mappings;

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
}
