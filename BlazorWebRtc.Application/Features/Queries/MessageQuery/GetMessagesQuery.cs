using BlazorWebRtc.Application.DTO.Message;
using MediatR;

namespace BlazorWebRtc.Application.Features.Queries.MessageQuery;

public class GetMessagesQuery:IRequest<List<MessageDto>>
{
    public Guid MessageUserId { get; set; }
}
