using BlazorWebRtc.Client.Models.Request;
using BlazorWebRtc.Client.Models.Response;

namespace BlazorWebRtc.Client.Services.Abstract
{
    public interface IAccountService
    {
        Task<UserResponseModel> SignUp(RegisterCommand command);
    }
}


