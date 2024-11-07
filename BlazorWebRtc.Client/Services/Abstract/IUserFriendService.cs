using BlazorWebRtc.Client.Models.Response;

namespace BlazorWebRtc.Client.Services.Abstract;

public interface IUserFriendService
{
    Task<List<UserDtoResponseModel>> GetAllFriendsByUser();
}
