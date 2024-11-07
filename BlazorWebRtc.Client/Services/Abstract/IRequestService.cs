﻿using BlazorWebRtc.Client.Models.Request;
using BlazorWebRtc.Client.Models.Response;

namespace BlazorWebRtc.Client.Services.Abstract;

public interface IRequestService
{
    Task<ResponseModel> SendFriendshipRequest(RequestFriendshipCommand command);
}