using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class PostcardCollectionSeeder
{
    public static IEnumerable<PostcardCollection> GetPostcardCollectionSeeder(DataContext dataContext)
    {
        List<User> users = dataContext.Users.ToList();
        List<PostcardData> postcardData = dataContext.PostcardData.ToList();
        IEnumerable<PostcardCollection> postcardCollections = new List<PostcardCollection>()
        {
            new PostcardCollection()
            {
                UserId = users[0].Id,
                PostcardDataId = postcardData[0].Id,
            },
            new PostcardCollection()
            {
                UserId = users[1].Id,
                PostcardDataId = postcardData[1].Id,
            },
            new PostcardCollection()
            {
                UserId = users[13].Id,
                PostcardDataId = postcardData[2].Id,
            },
            new PostcardCollection()
            {
                UserId = users[5].Id,
                PostcardDataId = postcardData[3].Id,
            },
            new PostcardCollection()
            {
                UserId = users[18].Id,
                PostcardDataId = postcardData[4].Id,
            },
            new PostcardCollection()
            {
                UserId = users[9].Id,
                PostcardDataId = postcardData[5].Id,
            },
            new PostcardCollection()
            {
                UserId = users[2].Id,
                PostcardDataId = postcardData[6].Id,
            },
            new PostcardCollection()
            {
                UserId = users[20].Id,
                PostcardDataId = postcardData[7].Id,
            },
            new PostcardCollection()
            {
                UserId = users[7].Id,
                PostcardDataId = postcardData[8].Id,
            },
            new PostcardCollection()
            {
                UserId = users[14].Id,
                PostcardDataId = postcardData[9].Id,
            },
            new PostcardCollection()
            {
                UserId = users[3].Id,
                PostcardDataId = postcardData[10].Id,
            },
            new PostcardCollection()
            {
                UserId = users[19].Id,
                PostcardDataId = postcardData[11].Id,
            },
            new PostcardCollection()
            {
                UserId = users[6].Id,
                PostcardDataId = postcardData[12].Id,
            },
            new PostcardCollection()
            {
                UserId = users[11].Id,
                PostcardDataId = postcardData[13].Id,
            },
            new PostcardCollection()
            {
                UserId = users[4].Id,
                PostcardDataId = postcardData[14].Id,
            },
        };

        return postcardCollections;
    }
}
