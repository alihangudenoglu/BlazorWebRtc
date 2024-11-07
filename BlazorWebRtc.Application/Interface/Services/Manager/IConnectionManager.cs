﻿namespace BlazorWebRtc.Application.Interface.Services.Manager;

public interface IConnectionManager
{
    void AddConnection(string userId, string connectionId);
    List<string> GetAllConnections();
    string GetConnection(string userId);
    IEnumerable<string> GetConnections(string userId);
    List<string> GetSpecificConnections();
    List<string> GetAllUserIds();
    void RemoveConnection(string connectionId);
}
