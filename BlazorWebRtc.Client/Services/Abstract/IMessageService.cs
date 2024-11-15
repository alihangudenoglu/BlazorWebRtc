﻿using BlazorWebRtc.Client.Models.Request;
using BlazorWebRtc.Client.Models.Response;

namespace BlazorWebRtc.Client.Services.Abstract;

public interface IMessageService
{
    Task<bool> SendMessage(SendMessageModel model);
    Task<List<MessageListResponseModel>> GetMessageList(MessageQueryModel model);
}
