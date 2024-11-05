using BlazorWebRtc.Application.Features.Commands.Upload;
using BlazorWebRtc.Application.Interface.Services;
using BlazorWebRtc.Application.Models;
using MediatR;

namespace BlazorWebRtc.Application.Services;

public class UploadService : IUploadService
{
    private readonly IMediator _mediator;
    private readonly BaseResponseModel _responseModel;

    public UploadService(IMediator mediator, BaseResponseModel responseModel)
    {
        _mediator = mediator;
        _responseModel = responseModel;
    }

    public async Task<BaseResponseModel> UploadFile(UploadCommand command)
    {
        var result = await _mediator.Send(command);
        if (result)
        {
            _responseModel.IsSuccess = true;
            return _responseModel;
        }
        _responseModel.IsSuccess = false;
        return _responseModel;
    }
}
