using AutoMapper;
using BlazorWebRtc.Application.DTO.Message;
using BlazorWebRtc.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc.Application.Features.Queries.MessageQuery;

public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, List<MessageDto>>
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetMessagesHandler(IHttpContextAccessor contextAccessor, AppDbContext appDbContext, IMapper mapper)
    {
        _contextAccessor = contextAccessor;
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<List<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var messages = await _appDbContext
            .MessageRooms
            .Where(x=>(x.SenderUserId==userId || x.ReceiverUserId==userId) 
            && (x.SenderUserId==request.MessageUserId || x.ReceiverUserId==request.MessageUserId)).OrderBy(x=>x.CreateDate).ToListAsync();

        if (messages.Any())
        {
            var result = _mapper.Map<List<MessageDto>>(messages);
            return result;
        }
        return new List<MessageDto>();
    }
}
