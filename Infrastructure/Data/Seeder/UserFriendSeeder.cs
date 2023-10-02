using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class UserFriendSeeder
{
    public static IEnumerable<UserFriends> GetUserFriendsSeeder()
    {
        IEnumerable<UserFriends> address = new List<UserFriends>()
        {
            new UserFriends()// ID: 1
            {
                UserId = 1,
                FriendId = 2
            },
            new UserFriends()// ID: 2
            {
                UserId = 2,
                FriendId = 1
            },
            new UserFriends()// ID: 3
            {
                UserId = 18,
                FriendId = 14
            },
            new UserFriends()// ID: 4
            {
                UserId = 6,
                FriendId = 8
            },
            new UserFriends()// ID: 5
            {
                UserId = 11,
                FriendId = 4
            },
            new UserFriends()// ID: 6
            {
                UserId = 19,
                FriendId = 10
            },
            new UserFriends()// ID: 7
            {
                UserId = 3,
                FriendId = 15
            },
            new UserFriends()// ID: 8
            {
                UserId = 22,
                FriendId = 7
            },
            new UserFriends()// ID: 9
            {
                UserId = 13,
                FriendId = 16
            },
            new UserFriends()// ID: 10
            {
                UserId = 5,
                FriendId = 9
            },
            new UserFriends()// ID: 11
            {
                UserId = 20,
                FriendId = 12
            },
            new UserFriends()// ID: 12
            {
                UserId = 2,
                FriendId = 17
            },
            new UserFriends()// ID: 13
            {
                UserId = 8,
                FriendId = 3
            },
            new UserFriends()// ID: 14
            {
                UserId = 14,
                FriendId = 21
            },
            new UserFriends()// ID: 15
            {
                UserId = 7,
                FriendId = 19
            },
            new UserFriends()// ID: 16
            {
                UserId = 10,
                FriendId = 6
            },
            new UserFriends()// ID: 17
            {
                UserId = 16,
                FriendId = 5
            },
            new UserFriends()// ID: 18
            {
                UserId = 12,
                FriendId = 18
            },
            new UserFriends()// ID: 19
            {
                UserId = 4,
                FriendId = 11
            },
            new UserFriends()// ID: 20
            {
                UserId = 9,
                FriendId = 10
            },
            new UserFriends()// ID: 21
            {
                UserId = 15,
                FriendId = 2
            },
            new UserFriends()// ID: 22
            {
                UserId = 17,
                FriendId = 13
            },
            new UserFriends()// ID: 23
            {
                UserId = 22,
                FriendId = 9
            },
            new UserFriends()// ID: 24
            {
                UserId = 11,
                FriendId = 7
            },
            new UserFriends()// ID: 25
            {
                UserId = 6,
                FriendId = 10
            },
            new UserFriends()// ID: 26
            {
                UserId = 13,
                FriendId = 4
            },
            new UserFriends()// ID: 27
            {
                UserId = 21,
                FriendId = 8
            },
            new UserFriends()// ID: 28
            {
                UserId = 3,
                FriendId = 19
            },
            new UserFriends()// ID: 29
            {
                UserId = 5,
                FriendId = 16
            },
            new UserFriends()// ID: 30
            {
                UserId = 8,
                FriendId = 12
            },
            new UserFriends()// ID: 31
            {
                UserId = 2,
                FriendId = 14
            },
            new UserFriends()// ID: 32
            {
                UserId = 18,
                FriendId = 15
            },
            new UserFriends()// ID: 33
            {
                UserId = 10,
                FriendId = 17
            },
            new UserFriends()// ID: 34
            {
                UserId = 19,
                FriendId = 6
            },
            new UserFriends()// ID: 35
            {
                UserId = 7,
                FriendId = 22
            },
            new UserFriends()// ID: 36
            {
                UserId = 4,
                FriendId = 13
            },
            new UserFriends()// ID: 37
            {
                UserId = 9,
                FriendId = 21
            },
            new UserFriends()// ID: 38
            {
                UserId = 16,
                FriendId = 3
            },
        };
        return address;
    }
}
