using Application.Dto;
using Domain.Entities;

namespace Application.Mappings.Manual;

public static class PostcardCollectionMapper
{
    public static PostcardCollectionDto Map(IEnumerable<PostcardCollection> postcardCollections)
    {
        return new PostcardCollectionDto
        {
            UserId = postcardCollections.First().UserId,
            PostcardDataIds = postcardCollections.Select(postcardCollection => postcardCollection.PostcardDataId)
        };
    }
}
