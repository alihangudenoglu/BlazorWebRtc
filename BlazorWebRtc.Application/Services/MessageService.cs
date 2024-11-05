using Azure;
using BlazorWebRtc.Application.Features.Commands.MessageCommand.SendMessage;
using BlazorWebRtc.Application.Interface.Services;
using BlazorWebRtc.Application.Models;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlazorWebRtc.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMediator _mediator;
    private readonly BaseResponseModel _responseModel;

    public MessageService(IMediator mediator, BaseResponseModel responseModel)
    {
        _mediator = mediator;
        _responseModel = responseModel;
    }

    public async Task<BaseResponseModel> SendMessage(SendMessageCommand command)
    {
        var response = await _mediator.Send(command);
        if (response != null)
        {
            _responseModel.IsSuccess = true;
            return _responseModel;
        }

        _responseModel.IsSuccess = false;
        return _responseModel;
    }
}
