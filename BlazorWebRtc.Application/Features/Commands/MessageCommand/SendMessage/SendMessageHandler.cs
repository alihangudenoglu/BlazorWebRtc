using BlazorWebRtc.Domain;
using BlazorWebRtc.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlazorWebRtc.Application.Features.Commands.MessageCommand.SendMessage;

public class SendMessageHandler : IRequestHandler<SendMessageCommand, bool>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;
    private Guid userId;

    public SendMessageHandler(AppDbContext context, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        userId = Guid.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        MessageRoom message = new();
        message.SenderUserId = userId;
        message.ReceiverUserId = request.ReceiverUserId;
        message.MessageContent = request.MessageContent;

        await _context.MessageRooms.AddAsync(message);

        if (await _context.SaveChangesAsync(cancellationToken)>0)
        {
            return true;
        }
        return false;
    }
}
