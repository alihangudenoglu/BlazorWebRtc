using BlazorWebRtc.Domain;
using MediatR;

namespace BlazorWebRtc.Application.Features.Commands.RequestFeature;

public class RequestCommand:IRequest<bool>
{
    public Status Status { get; set; } = Status.pending;
    public Guid ReceiverUserId { get; set; }
}
