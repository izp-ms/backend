using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class UserStatSeeder
{
    public static IEnumerable<UserStat> GetUsersSeeder()
    {
        IEnumerable<UserStat> userStats = new List<UserStat>()
        {
            new UserStat()// ID: 1
            {
                PostcardsSent = 1,
                PostcardsReceived = 2,
                Score = 3,
            },
            new UserStat()// ID: 2
            {
                PostcardsSent = 3,
                PostcardsReceived = 2,
                Score = 5,
            },
            new UserStat()// ID: 3
            {
                PostcardsSent = 4,
                PostcardsReceived = 3,
                Score = 7,
            },
            new UserStat()// ID: 4
            {
                PostcardsSent = 5,
                PostcardsReceived = 2,
                Score = 7,
            },
            new UserStat()// ID: 5
            {
                PostcardsSent = 2,
                PostcardsReceived = 6,
                Score = 8,
            },
             new UserStat()// ID: 6
            {
                PostcardsSent = 4,
                PostcardsReceived = 2,
                Score = 6,
            },
            new UserStat()// ID: 7
            {
                PostcardsSent = 6,
                PostcardsReceived = 7,
                Score = 13,
            },
            new UserStat()// ID: 8
            {
                PostcardsSent = 3,
                PostcardsReceived = 7,
                Score = 10,
            },
            new UserStat()// ID: 9
            {
                PostcardsSent = 7,
                PostcardsReceived = 5,
                Score = 12,
            },
            new UserStat()// ID: 10
            {
                PostcardsSent = 2,
                PostcardsReceived = 4,
                Score = 6,
            },
            new UserStat()// ID: 11
            {
                PostcardsSent = 1,
                PostcardsReceived = 7,
                Score = 8,
            },
            new UserStat()// ID: 12
            {
                PostcardsSent = 5,
                PostcardsReceived = 3,
                Score = 8,
            },
            new UserStat()// ID: 13
            {
                PostcardsSent = 7,
                PostcardsReceived = 6,
                Score = 13,
            },
            new UserStat()// ID: 14
            {
                PostcardsSent = 3,
                PostcardsReceived = 4,
                Score = 7,
            },
            new UserStat()// ID: 15
            {
                PostcardsSent = 6,
                PostcardsReceived = 4,
                Score = 10,
            },
            new UserStat()// ID: 16
            {
                PostcardsSent = 4,
                PostcardsReceived = 6,
                Score = 10,
            },
            new UserStat()// ID: 17
            {
                PostcardsSent = 3,
                PostcardsReceived = 5,
                Score = 8,
            },
            new UserStat()// ID: 18
            {
                PostcardsSent = 2,
                PostcardsReceived = 7,
                Score = 9,
            },
            new UserStat()// ID: 19
            {
                PostcardsSent = 7,
                PostcardsReceived = 3,
                Score = 10,
            },
            new UserStat()// ID: 20
            {
                PostcardsSent = 1,
                PostcardsReceived = 5,
                Score = 6,
            },
            new UserStat()// ID: 21
            {
                PostcardsSent = 5,
                PostcardsReceived = 1,
                Score = 6,
            },
            new UserStat()// ID: 22
            {
                PostcardsSent = 6,
                PostcardsReceived = 2,
                Score = 8,
            },
        };
        return userStats;
    }
}
