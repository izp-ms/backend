using Application.Dto;
using Domain.Entities;

namespace Application.Mappings.Manual;

public static class FriendsMapper
{
    public static IEnumerable<FriendDto> MapFollowing(IEnumerable<UserFriends> userFriends)
    {
        return userFriends.Select(userFriend =>
        {
            return new FriendDto()
            {
                Id = userFriend.FriendId,
                NickName = userFriend.Friend.NickName,
                Email = userFriend.Friend.Email,
                CreatedAt = userFriend.Friend.CreatedAt,
                FirstName = userFriend.Friend.UsersDetails.FirstName,
                LastName = userFriend.Friend.UsersDetails.LastName,
                BirthDate = userFriend.Friend.UsersDetails.BirthDate,
                AvatarBase64 = userFriend.Friend.UsersDetails.AvatarBase64,
                BackgroundBase64 = userFriend.Friend.UsersDetails.BackgroundBase64,
                Description = userFriend.Friend.UsersDetails.Description,
                City = userFriend.Friend.Address.City,
                Country = userFriend.Friend.Address.Country,
                PostcardsSent = userFriend.Friend.UsersStats.PostcardsSent,
                PostcardsReceived = userFriend.Friend.UsersStats.PostcardsReceived,
                Score = userFriend.Friend.UsersStats.Score
            };
        });
    }

    public static IEnumerable<FriendDto> MapFollowers(IEnumerable<UserFriends> userFriends)
    {
        return userFriends.Select(userFriend =>
        {
            return new FriendDto()
            {
                Id = userFriend.UserId,
                NickName = userFriend.User.NickName,
                Email = userFriend.User.Email,
                CreatedAt = userFriend.User.CreatedAt,
                FirstName = userFriend.User.UsersDetails.FirstName,
                LastName = userFriend.User.UsersDetails.LastName,
                BirthDate = userFriend.User.UsersDetails.BirthDate,
                AvatarBase64 = userFriend.User.UsersDetails.AvatarBase64,
                BackgroundBase64 = userFriend.User.UsersDetails.BackgroundBase64,
                Description = userFriend.User.UsersDetails.Description,
                City = userFriend.User.Address.City,
                Country = userFriend.User.Address.Country,
                PostcardsSent = userFriend.User.UsersStats.PostcardsSent,
                PostcardsReceived = userFriend.User.UsersStats.PostcardsReceived,
                Score = userFriend.User.UsersStats.Score
            };
        });
    }
}