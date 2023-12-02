using Application.Dto;
using Application.Requests;
using Application.Response;

namespace Application.Interfaces;

public interface IUserFriendsService
{
    Task<PaginationResponse<FriendDto>> GetPaginatedFollowing(PaginationRequest pagination, FiltersUserRequest filters);
    Task<PaginationResponse<FriendDto>> GetPaginatedFollowers(PaginationRequest pagination, FiltersUserRequest filters);
    Task<bool> IsFollowing(int userId, int friendId);
    Task<FriendDto> AddNewFriend(UserFriendRequest addUserFriendRequest);
    Task<FriendDto> DeleteFriend(UserFriendRequest deleteUserFriendRequest);
}
