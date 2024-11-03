using BlazorWebRtc.Application.DTO;
using MediatR;

namespace BlazorWebRtc.Application.Features.Queries.UserInfo;

public class UserListQuery:IRequest<List<UserDto>>
{

}
