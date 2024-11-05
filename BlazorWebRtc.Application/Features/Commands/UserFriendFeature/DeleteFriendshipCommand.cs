using MediatR;

namespace BlazorWebRtc.Application.Features.Commands.Feature;

public class DeleteFriendshipCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
