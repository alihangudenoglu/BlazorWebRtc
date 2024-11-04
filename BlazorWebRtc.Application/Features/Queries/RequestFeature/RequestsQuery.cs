using BlazorWebRtc.Application.DTO.Request;
using MediatR;

namespace BlazorWebRtc.Application.Features.Queries.RequestFeature;

public class RequestsQuery:IRequest<List<GetRequestDto>>
{
    public Guid UserId { get; set; }
}
