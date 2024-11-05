using MediatR;

namespace BlazorWebRtc.Application.Features.Commands.Feature;

public class UserFriendCommand:IRequest<bool>
{
    public Guid RequesterId { get; set; }
    public Guid ReceiverUserId { get; set; }
}
