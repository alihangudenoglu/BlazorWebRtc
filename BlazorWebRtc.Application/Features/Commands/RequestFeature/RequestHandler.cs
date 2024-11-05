using BlazorWebRtc.Domain;
using BlazorWebRtc.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc.Application.Features.Commands.RequestFeature;
public class RequestHandler : IRequestHandler<RequestCommand, bool>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;
    private string userId;

    public RequestHandler(AppDbContext context, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Handle(RequestCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.ReceiverUserId && request.Status==Status.pending);

        if (result is not null)
        {
            userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Request requestObj = new Request();
            requestObj.ReceiverUserId = request.ReceiverUserId;
            requestObj.SenderUserId = Guid.Parse(userId);
            requestObj.Status = request.Status;

            await _context.Requests.AddAsync(requestObj);

            if (await _context.SaveChangesAsync(cancellationToken) > 0)
            {
                return true;
            }
        }

        return false;
    }
}
