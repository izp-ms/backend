using Domain.Entities;

namespace Application.Mappings.Manual;

public static class PostcardMapper
{
    public static PostcardDto Map(Postcard postcard, int userId)
    {
        return new PostcardDto()
        {
            Id = postcard.Id,
            Title = postcard.Title,
            Content = postcard.Content,
            PostcardDataId = postcard.PostcardDataId,
            Type = postcard.Type,
            CreatedAt = postcard.CreatedAt,
            UserId = userId,
            IsSent = postcard.IsSent
        };
    }
}
