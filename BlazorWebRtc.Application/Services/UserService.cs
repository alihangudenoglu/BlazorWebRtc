using BlazorWebRtc.Application.Features.Queries.UserInfo;
using BlazorWebRtc.Application.Interface.Services;
using BlazorWebRtc.Application.Models;
using MediatR;

namespace BlazorWebRtc.Application.Services;

public class UserService : IUserService
{
    private readonly IMediator _mediator;
    private readonly BaseResponseModel _responseModel;

    public UserService(IMediator mediator, BaseResponseModel responseModel)
    {
        _mediator = mediator;
        _responseModel = responseModel;
    }

    public async Task<BaseResponseModel> GetUserList()
    {
        UserListQuery query = new UserListQuery();
        var response= await _mediator.Send(query);

        if (response ==null)
        {
            _responseModel.IsSuccess = false;
            return _responseModel;
        }

        _responseModel.IsSuccess = true;
        _responseModel.Data=response;
        return _responseModel;

    }
}
