using Application.Dto;
using Domain.Entities;

public static class PostcardWithDataDtoMapper
{
    public static IEnumerable<PostcardWithDataDto> Map(IEnumerable<Postcard> postcards, int userId)
    {
        return postcards.Select(postcard =>
        {
            return new PostcardWithDataDto()
            {
                Id = postcard.Id,
                Title = postcard.Title,
                Content = postcard.Content,
                PostcardDataId = postcard.PostcardDataId,
                Type = postcard.Type,
                CreatedAt = postcard.CreatedAt,
                UserId = userId,
                IsSent = postcard.IsSent,
                ImageBase64 = postcard.PostcardData.ImageBase64,
                Country = postcard.PostcardData.Country,
                City = postcard.PostcardData.City,
                PostcardDataTitle = postcard.PostcardData.Title,
                Longitude = postcard.PostcardData.Longitude,
                Latitude = postcard.PostcardData.Latitude,
                CollectRangeInMeters = postcard.PostcardData.CollectRangeInMeters
            };
        });
    }
}
