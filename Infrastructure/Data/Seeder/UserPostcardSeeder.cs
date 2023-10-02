using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class UserPostcardSeeder
{
    public static IEnumerable<UserPostcard> GetUserPostcardsSeeder(DataContext dataContext)
    {
        var users = dataContext.Users.ToList();
        var postcard = dataContext.Postcards.ToList();
        IEnumerable<UserPostcard> address = new List<UserPostcard>()
        {
            new UserPostcard()
            {
                UserId = users[0].Id,
                PostcardId = postcard[0].Id,
                ReceivedAt = new DateTime(2022, 7, 25)
            },
            new UserPostcard()
            {
                UserId = users[1].Id,
                PostcardId = postcard[1].Id,
                ReceivedAt = new DateTime(2022, 7, 26)
            },
            new UserPostcard()
            {
                UserId = users[13].Id,
                PostcardId = postcard[2].Id,
                ReceivedAt = new DateTime(2020, 1, 15)
            },
            new UserPostcard()
            {
                UserId = users[5].Id,
                PostcardId = postcard[3].Id,
                ReceivedAt = new DateTime(2022, 6, 10)
            },
            new UserPostcard()
            {
                UserId = users[18].Id,
                PostcardId = postcard[4].Id,
                ReceivedAt = new DateTime(2021, 3, 21)
            },
            new UserPostcard()
            {
                UserId = users[9].Id,
                PostcardId = postcard[5].Id,
                ReceivedAt = new DateTime(2023, 11, 5)
            },
            new UserPostcard()
            {
                UserId = users[2].Id,
                PostcardId = postcard[6].Id,
                ReceivedAt = new DateTime(2021, 9, 3)
            },
            new UserPostcard()
            {
                UserId = users[20].Id,
                PostcardId = postcard[7].Id,
                ReceivedAt = new DateTime(2020, 7, 12)
            },
            new UserPostcard()
            {
                UserId = users[7].Id,
                PostcardId = postcard[8].Id,
                ReceivedAt = new DateTime(2021, 4, 30)
            },
            new UserPostcard()
            {
                UserId = users[14].Id,
                PostcardId = postcard[9].Id,
                ReceivedAt = new DateTime(2022, 8, 25)
            },
            new UserPostcard()
            {
                UserId = users[3].Id,
                PostcardId = postcard[10].Id,
                ReceivedAt = new DateTime(2019, 2, 8)
            },
            new UserPostcard()
            {
                UserId = users[19].Id,
                PostcardId = postcard[11].Id,
                ReceivedAt = new DateTime(2019, 11, 7)
            },
            new UserPostcard()
            {
                UserId = users[6].Id,
                PostcardId = postcard[12].Id,
                ReceivedAt = new DateTime(2019, 12, 17)
            },
            new UserPostcard()
            {
                UserId = users[11].Id,
                PostcardId = postcard[13].Id,
                ReceivedAt = new DateTime(2020, 2, 3),
            },
            new UserPostcard()
            {
                UserId = users[4].Id,
                PostcardId = postcard[14].Id,
                ReceivedAt = new DateTime(2020, 7, 21),
            },
        };
        return address;
    }
}
