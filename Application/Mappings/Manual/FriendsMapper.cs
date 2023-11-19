using Application.Dto;
using Domain.Entities;

namespace Application.Mappings.Manual;

public static class FriendsMapper
{
    public static IEnumerable<FriendDto> Map(IEnumerable<UserFriends> userFriends)
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
                Description = userFriend.Friend.UsersDetails.Description,
                City = userFriend.Friend.Address.City,
                Country = userFriend.Friend.Address.Country,
                PostcardsSent = userFriend.Friend.UsersStats.PostcardsSent,
                PostcardsReceived = userFriend.Friend.UsersStats.PostcardsReceived,
                Score = userFriend.Friend.UsersStats.Score
            };
        });
    }
}