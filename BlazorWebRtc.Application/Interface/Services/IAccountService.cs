using BlazorWebRtc.Application.Features.Commands.Account.Login;
using BlazorWebRtc.Application.Features.Commands.Account.Register;
using BlazorWebRtc.Application.Models;

namespace BlazorWebRtc.Application.Interface.Services;

public interface IAccountService
{
    Task<BaseResponseModel> SignUp(RegisterCommand command);
    Task<BaseResponseModel> SignIn(LoginCommand command);
}
