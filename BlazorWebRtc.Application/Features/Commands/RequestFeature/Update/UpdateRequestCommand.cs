using BlazorWebRtc.Domain;
using MediatR;

namespace BlazorWebRtc.Application.Features.Commands.RequestFeature.Update;

public class UpdateRequestCommand : IRequest<Request>
{
    public Guid RequestId { get; set; }
    public Status Status { get; set; }
}
