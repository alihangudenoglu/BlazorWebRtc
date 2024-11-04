using BlazorWebRtc.Application.Features.Commands.RequestFeature;
using BlazorWebRtc.Application.Features.Queries.RequestFeature;
using BlazorWebRtc.Application.Interface.Services;
using BlazorWebRtc.Application.Models;
using MediatR;

namespace BlazorWebRtc.Application.Services;

public class RequestService : IRequestService
{
    private readonly IMediator _mediator;
    private readonly BaseResponseModel _responseModel;

    public RequestService(IMediator mediator, BaseResponseModel responseModel)
    {
        _mediator = mediator;
        _responseModel = responseModel;
    }

    public async Task<BaseResponseModel> GetRequestList(RequestsQuery query)
    {
        var result = await _mediator.Send(query);

        if (result==null)
        {
            _responseModel.IsSuccess = false;
            return _responseModel;
        }
        _responseModel.IsSuccess = false;
        _responseModel.Data=result;
        return _responseModel;
    }

    public async Task<BaseResponseModel> SendRequest(RequestCommand requestCommand)
    {
        var result = await _mediator.Send(requestCommand);

        if (result)
        {
            _responseModel.IsSuccess = true;
            return _responseModel;
        }
        _responseModel.IsSuccess = false;
        return _responseModel;

    }
}
