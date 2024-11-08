using MediatR;

namespace BlazorWebRtc.Application.Features.Commands.MessageCommand.SendMessage;

public class SendMessageCommand:IRequest<bool>
{
    public string MessageContent { get; set; }
    public Guid ReceiverUserId { get; set; }
}
