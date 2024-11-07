using BlazorWebRtc.Application.Interface.Services.Manager;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BlazorWebRtc.Application.Hubs;

public class UserHub:Hub
{
    private readonly IConnectionManager _connectionManager;

    public UserHub(IConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public override Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;

        var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();

        _connectionManager.AddConnection(userId,connectionId);
        return base.OnConnectedAsync();
    }

    public override  Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;

        _connectionManager.RemoveConnection(connectionId);

        return base.OnDisconnectedAsync(exception);
    }
}
