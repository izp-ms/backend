using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class UserPostcardSeeder
{
    public static IEnumerable<UserPostcard> GetUserPostcardsSeeder()
    {
        IEnumerable<UserPostcard> address = new List<UserPostcard>()
        {
            new UserPostcard()// ID: 1
            {
                UserId = 1,
                PostcardId = 1,
                ReceivedAt = new DateTime(2022, 7, 25)
            },
            new UserPostcard()// ID: 2
            {
                UserId = 2,
                PostcardId = 2,
                ReceivedAt = new DateTime(2022, 7, 26)
            },
            new UserPostcard()// ID: 3
            {
                UserId = 14,
                PostcardId = 3,
                ReceivedAt = new DateTime(2020, 1, 15)
            },
            new UserPostcard()// ID: 4
            {
                UserId = 6,
                PostcardId = 4,
                ReceivedAt = new DateTime(2022, 6, 10)
            },
            new UserPostcard()// ID: 5
            {
                UserId = 19,
                PostcardId = 5,
                ReceivedAt = new DateTime(2021, 3, 21)
            },
            new UserPostcard()// ID: 6
            {
                UserId = 10,
                PostcardId = 6,
                ReceivedAt = new DateTime(2023, 11, 5)
            },
            new UserPostcard()// ID: 7
            {
                UserId = 3,
                PostcardId = 7,
                ReceivedAt = new DateTime(2021, 9, 3)
            },
            new UserPostcard()// ID: 8
            {
                UserId = 21,
                PostcardId = 8,
                ReceivedAt = new DateTime(2020, 7, 12)
            },
            new UserPostcard()// ID: 9
            {
                UserId = 8,
                PostcardId = 9,
                ReceivedAt = new DateTime(2021, 4, 30)
            },
            new UserPostcard()// ID: 10
            {
                UserId = 15,
                PostcardId = 10,
                ReceivedAt = new DateTime(2022, 8, 25)
            },
            new UserPostcard()// ID: 11
            {
                UserId = 4,
                PostcardId = 11,
                ReceivedAt = new DateTime(2019, 2, 8)
            },
            new UserPostcard()// ID: 12
            {
                UserId = 20,
                PostcardId = 12,
                ReceivedAt = new DateTime(2019, 11, 7)
            },
            new UserPostcard()// ID: 13
            {
                UserId = 7,
                PostcardId = 13,
                ReceivedAt = new DateTime(2019, 12, 17)
            },
            new UserPostcard()// ID: 14
            {
                UserId = 12,
                PostcardId = 14,
                ReceivedAt = new DateTime(2020, 2, 3),
            },
            new UserPostcard()// ID: 15
            {
                UserId = 5,
                PostcardId = 15,
                ReceivedAt = new DateTime(2020, 7, 21),
            },
        };
        return address;
    }
}
