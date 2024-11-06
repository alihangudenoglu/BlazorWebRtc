using BlazorWebRtc.Client.Models.Response;

namespace BlazorWebRtc.Client.Services.Abstract;

public interface IUserInfoService
{
    Task<List<UserDtoResponseModel>> GetUserList();
}
