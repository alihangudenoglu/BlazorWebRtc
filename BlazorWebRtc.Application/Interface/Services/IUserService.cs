using BlazorWebRtc.Application.Models;

namespace BlazorWebRtc.Application.Interface.Services;

public interface IUserService
{
    Task<BaseResponseModel> GetUserList();
}
