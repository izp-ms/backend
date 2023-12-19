using Application.Dto;
using Domain.Entities;

namespace Application.Mappings.Manual;

public static class UsersMapper
{
    public static IEnumerable<UserDto> Map(IEnumerable<User> users)
    {
        return users.Select(user =>
        {
            return new UserDto()
            {
                Id = user.Id,
                NickName = user.NickName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                FirstName = user.UsersDetails.FirstName,
                LastName = user.UsersDetails.LastName,
                BirthDate = user.UsersDetails.BirthDate,
                AvatarBase64 = user.UsersDetails.AvatarBase64,
                BackgroundBase64 = user.UsersDetails.BackgroundBase64,
                Description = user.UsersDetails.Description,
                City = user.Address.City,
                Country = user.Address.Country,
                PostcardsSent = user.UsersStats.PostcardsSent,
                PostcardsReceived = user.UsersStats.PostcardsReceived,
                Score = user.UsersStats.Score,
            };
        });
    }
}