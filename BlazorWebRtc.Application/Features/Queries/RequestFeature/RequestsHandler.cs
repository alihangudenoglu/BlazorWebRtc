using BlazorWebRtc.Application.DTO.Request;
using BlazorWebRtc.Domain;
using BlazorWebRtc.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc.Application.Features.Queries.RequestFeature;

public class RequestsHandler : IRequestHandler<RequestsQuery, List<GetRequestDto>>
{
    private readonly AppDbContext _context;

    public RequestsHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetRequestDto>> Handle(RequestsQuery request, CancellationToken cancellationToken)
    {
        var requests = await _context.Requests.Include(x => x.SenderUser).Where(x => x.ReceiverUserId == request.UserId && x.Status==Status.pending).ToListAsync(cancellationToken);

        List<GetRequestDto> requestList = new();
        foreach (var item in requests)
        {
            GetRequestDto requestDto = new();
            requestDto.ProfilePicture = item.SenderUser.ProfilePicture;
            requestDto.UserName = item.SenderUser.UserName;
            requestDto.Email = item.SenderUser.Email;
            requestDto.UserId = item.SenderUserId;
            requestDto.Id = item.Id;
            requestList.Add(requestDto);
        }

        if (requestList.Any())
        {
            return requestList;
        }

        return null;
    }
}
