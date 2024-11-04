using BlazorWebRtc.Application.Features.Commands.RequestFeature;
using BlazorWebRtc.Application.Features.Queries.RequestFeature;
using BlazorWebRtc.Application.Models;

namespace BlazorWebRtc.Application.Interface.Services;

public interface IRequestService
{
    Task<BaseResponseModel> SendRequest(RequestCommand requestCommand);
    Task<BaseResponseModel> GetRequestList(RequestsQuery query);
}
