using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class UserStatSeeder
{
    public static IEnumerable<UserStat> GetUsersStatSeeder(DataContext dataContext)
    {
        var users = dataContext.Users.ToList();
        IEnumerable<UserStat> userStats = new List<UserStat>()
        {
            new UserStat()
            {
                Id = users[0].Id,
                PostcardsSent = 1,
                PostcardsReceived = 2,
                Score = 3,
            },
            new UserStat()
            {
                Id = users[1].Id,
                PostcardsSent = 3,
                PostcardsReceived = 2,
                Score = 5,
            },
            new UserStat()
            {
                Id = users[2].Id,
                PostcardsSent = 4,
                PostcardsReceived = 3,
                Score = 7,
            },
            new UserStat()
            {
                Id = users[3].Id,
                PostcardsSent = 5,
                PostcardsReceived = 2,
                Score = 7,
            },
            new UserStat()
            {
                Id = users[4].Id,
                PostcardsSent = 2,
                PostcardsReceived = 6,
                Score = 8,
            },
             new UserStat()
            {
                Id = users[5].Id,
                PostcardsSent = 4,
                PostcardsReceived = 2,
                Score = 6,
            },
            new UserStat()
            {
                Id = users[6].Id,
                PostcardsSent = 6,
                PostcardsReceived = 7,
                Score = 13,
            },
            new UserStat()
            {
                Id = users[7].Id,
                PostcardsSent = 3,
                PostcardsReceived = 7,
                Score = 10,
            },
            new UserStat()
            {
                Id = users[8].Id,
                PostcardsSent = 7,
                PostcardsReceived = 5,
                Score = 12,
            },
            new UserStat()
            {
                Id = users[9].Id,
                PostcardsSent = 2,
                PostcardsReceived = 4,
                Score = 6,
            },
            new UserStat()
            {
                Id = users[10].Id,
                PostcardsSent = 1,
                PostcardsReceived = 7,
                Score = 8,
            },
            new UserStat()
            {
                Id = users[11].Id,
                PostcardsSent = 5,
                PostcardsReceived = 3,
                Score = 8,
            },
            new UserStat()
            {
                Id = users[12].Id,
                PostcardsSent = 7,
                PostcardsReceived = 6,
                Score = 13,
            },
            new UserStat()
            {
                Id = users[13].Id,
                PostcardsSent = 3,
                PostcardsReceived = 4,
                Score = 7,
            },
            new UserStat()
            {
                Id = users[14].Id,
                PostcardsSent = 6,
                PostcardsReceived = 4,
                Score = 10,
            },
            new UserStat()
            {
                Id = users[15].Id,
                PostcardsSent = 4,
                PostcardsReceived = 6,
                Score = 10,
            },
            new UserStat()
            {
                Id = users[16].Id,
                PostcardsSent = 3,
                PostcardsReceived = 5,
                Score = 8,
            },
            new UserStat()
            {
                Id = users[17].Id,
                PostcardsSent = 2,
                PostcardsReceived = 7,
                Score = 9,
            },
            new UserStat()
            {
                Id = users[18].Id,
                PostcardsSent = 7,
                PostcardsReceived = 3,
                Score = 10,
            },
            new UserStat()
            {
                Id = users[19].Id,
                PostcardsSent = 1,
                PostcardsReceived = 5,
                Score = 6,
            },
            new UserStat()
            {
                Id = users[20].Id,
                PostcardsSent = 5,
                PostcardsReceived = 1,
                Score = 6,
            },
            new UserStat()
            {
                Id = users[21].Id,
                PostcardsSent = 6,
                PostcardsReceived = 2,
                Score = 8,
            },
        };
        return userStats;
    }
}
