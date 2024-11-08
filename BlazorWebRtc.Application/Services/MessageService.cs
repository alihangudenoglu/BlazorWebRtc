using Azure;
using BlazorWebRtc.Application.DTO.Message;
using BlazorWebRtc.Application.Features.Commands.MessageCommand.SendMessage;
using BlazorWebRtc.Application.Features.Queries.MessageQuery;
using BlazorWebRtc.Application.Hubs;
using BlazorWebRtc.Application.Interface.Services;
using BlazorWebRtc.Application.Interface.Services.Manager;
using BlazorWebRtc.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlazorWebRtc.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMediator _mediator;
    private readonly BaseResponseModel _responseModel;
    private readonly IHubContext<UserHub> _hubContext;
    private readonly IConnectionManager _connectionManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public MessageService(IMediator mediator, BaseResponseModel responseModel, IHubContext<UserHub> hubContext, IConnectionManager connectionManager, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _responseModel = responseModel;
        _hubContext = hubContext;
        _connectionManager = connectionManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<BaseResponseModel> GetlistMessage(GetMessagesQuery query)
    {
        var response = await _mediator.Send(query);
        if (response.Count > 0)
        {
            _responseModel.IsSuccess = true;
            _responseModel.Data=response;
            return _responseModel;
        }

        _responseModel.IsSuccess = false;
        return _responseModel;
    }

    public async Task<BaseResponseModel> SendMessage(SendMessageCommand command)
    {
        var response = await _mediator.Send(command);
        if (response != null)
        {
            List<MessageDto> messages = new List<MessageDto>();       

            var senderUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<string> _userIds = new List<string>() { senderUserId, command.ReceiverUserId.ToString() };

            GetMessagesQuery query = new GetMessagesQuery();
            query.MessageUserId=command.ReceiverUserId;
            var obj = await _mediator.Send(query);
            var serializeMessages = JsonConvert.SerializeObject(obj);

            var userIds = _connectionManager.GetConnectionByUserId(_userIds);

            await _hubContext.Clients.Clients(userIds).SendAsync("ReceiveMessage", serializeMessages);
            _responseModel.IsSuccess = true;
            return _responseModel;
        }

        _responseModel.IsSuccess = false;
        return _responseModel;
    }
}
