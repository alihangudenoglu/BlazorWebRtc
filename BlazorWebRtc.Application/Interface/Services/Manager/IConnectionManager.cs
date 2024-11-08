namespace BlazorWebRtc.Application.Interface.Services.Manager;

public interface IConnectionManager
{
    void AddConnection(string userId, string connectionId);
    List<string> GetAllConnections();
    string GetConnection(string userId);
    IEnumerable<string> GetConnections(string userId);
    List<string> GetSpecificConnections();
    List<string> GetAllUserIds();
    List<string> GetConnectionByUserId(List<string> userIds);
    string GetConnectionByUserIdSingleObj(string userId);
    void RemoveConnection(string connectionId);
}
