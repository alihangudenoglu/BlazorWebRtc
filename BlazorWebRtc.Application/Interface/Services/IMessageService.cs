using BlazorWebRtc.Application.Features.Commands.MessageCommand.SendMessage;
using BlazorWebRtc.Application.Features.Queries.MessageQuery;
using BlazorWebRtc.Application.Models;

namespace BlazorWebRtc.Application.Interface.Services;

public interface IMessageService
{
    Task<BaseResponseModel> SendMessage(SendMessageCommand command);
    Task<BaseResponseModel> GetlistMessage(GetMessagesQuery query);
}
