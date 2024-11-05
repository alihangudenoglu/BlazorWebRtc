using BlazorWebRtc.Application.Features.Commands.Feature;
using BlazorWebRtc.Application.Features.Queries.UserFriend;
using BlazorWebRtc.Application.Models;

namespace BlazorWebRtc.Application.Interface.Services;

public interface IUserFriendService
{
    Task<BaseResponseModel> AddFriendship(UserFriendCommand command);
    Task<BaseResponseModel> DeleteFriendship(DeleteFriendshipCommand command);
    Task<BaseResponseModel> GetFriendshipList(UserFriendListCommand command);
}
