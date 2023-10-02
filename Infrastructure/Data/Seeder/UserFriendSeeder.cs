using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class UserFriendSeeder
{
    public static IEnumerable<UserFriends> GetUserFriendsSeeder(DataContext dataContext)
    {
        var users = dataContext.Users.ToList();
        IEnumerable<UserFriends> address = new List<UserFriends>()
        {
            new UserFriends()
            {
                UserId = users[0].Id,
                FriendId = users[1].Id
            },
            new UserFriends()
            {
                UserId = users[1].Id,
                FriendId = users[0].Id
            },
            new UserFriends()
            {
                UserId = users[17].Id,
                FriendId = users[13].Id
            },
            new UserFriends()
            {
                UserId = users[5].Id,
                FriendId = users[7].Id
            },
            new UserFriends()
            {
                UserId = users[10].Id,
                FriendId = users[3].Id
            },
            new UserFriends()
            {
                UserId = users[18].Id,
                FriendId = users[9].Id
            },
            new UserFriends()
            {
                UserId = users[2].Id,
                FriendId = users[14].Id
            },
            new UserFriends()
            {
                UserId = users[21].Id,
                FriendId = users[6].Id
            },
            new UserFriends()
            {
                UserId = users[12].Id,
                FriendId = users[15].Id
            },
            new UserFriends()
            {
                UserId = users[4].Id,
                FriendId = users[8].Id
            },
            new UserFriends()
            {
                UserId = users[19].Id,
                FriendId = users[11].Id
            },
            new UserFriends()
            {
                UserId = users[1].Id,
                FriendId = users[16].Id
            },
            new UserFriends()
            {
                UserId = users[7].Id,
                FriendId = users[2].Id
            },
            new UserFriends()
            {
                UserId = users[13].Id,
                FriendId = users[20].Id
            },
            new UserFriends()
            {
                UserId = users[6].Id,
                FriendId = users[18].Id
            },
            new UserFriends()
            {
                UserId = users[9].Id,
                FriendId = users[5].Id
            },
            new UserFriends()
            {
                UserId = users[15].Id,
                FriendId = users[4].Id
            },
            new UserFriends()
            {
                UserId = users[11].Id,
                FriendId = users[17].Id
            },
            new UserFriends()
            {
                UserId = users[3].Id,
                FriendId = users[10].Id
            },
            new UserFriends()
            {
                UserId = users[8].Id,
                FriendId = users[9].Id
            },
            new UserFriends()
            {
                UserId = users[14].Id,
                FriendId = users[1].Id
            },
            new UserFriends()
            {
                UserId = users[16].Id,
                FriendId = users[12].Id
            },
            new UserFriends()
            {
                UserId = users[21].Id,
                FriendId = users[8].Id
            },
            new UserFriends()
            {
                UserId = users[10].Id,
                FriendId = users[6].Id
            },
            new UserFriends()
            {
                UserId = users[5].Id,
                FriendId = users[9].Id
            },
            new UserFriends()
            {
                UserId = users[12].Id,
                FriendId = users[3].Id
            },
            new UserFriends()
            {
                UserId = users[20].Id,
                FriendId = users[7].Id
            },
            new UserFriends()
            {
                UserId = users[2].Id,
                FriendId = users[18].Id
            },
            new UserFriends()
            {
                UserId = users[4].Id,
                FriendId = users[15].Id
            },
            new UserFriends()
            {
                UserId = users[7].Id,
                FriendId = users[11].Id
            },
            new UserFriends()
            {
                UserId = users[1].Id,
                FriendId = users[13].Id
            },
            new UserFriends()
            {
                UserId = users[17].Id,
                FriendId = users[14].Id
            },
            new UserFriends()
            {
                UserId = users[9].Id,
                FriendId = users[16].Id
            },
            new UserFriends()
            {
                UserId = users[18].Id,
                FriendId = users[5].Id
            },
            new UserFriends()
            {
                UserId = users[6].Id,
                FriendId = users[21].Id
            },
            new UserFriends()
            {
                UserId = users[3].Id,
                FriendId = users[12].Id
            },
            new UserFriends()
            {
                UserId = users[8].Id,
                FriendId = users[20].Id
            },
            new UserFriends()
            {
                UserId = users[15].Id,
                FriendId = users[2].Id
            },
        };
        return address;
    }
}
