using BlazorWebRtc.Application.DTO.UserFriend;
using MediatR;

namespace BlazorWebRtc.Application.Features.Queries.UserFriend;

public class UserFriendListCommand:IRequest<List<UserFriendDto>>
{
}
