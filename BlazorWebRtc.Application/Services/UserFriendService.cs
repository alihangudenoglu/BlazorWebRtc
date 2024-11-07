using BlazorWebRtc.Application.Features.Commands.Feature;
using BlazorWebRtc.Application.Features.Queries.UserFriend;
using BlazorWebRtc.Application.Interface.Services;
using BlazorWebRtc.Application.Models;
using MediatR;

namespace BlazorWebRtc.Application.Services;

public class UserFriendService : IUserFriendService
{
    private readonly IMediator _mediator;
    private readonly BaseResponseModel _responseModel;

    public UserFriendService(IMediator mediator, BaseResponseModel baseResponseModel)
    {
        _mediator = mediator;
        _responseModel = baseResponseModel;
    }

    public async Task<BaseResponseModel> AddFriendship(UserFriendCommand command)
    {
        var result = await _mediator.Send(command);

        if (result)
        {
            _responseModel.IsSuccess=true;
            return _responseModel;
        }
        _responseModel.IsSuccess = false;
        return _responseModel;
    }

    public async Task<BaseResponseModel> DeleteFriendship(DeleteFriendshipCommand command)
    {
        var result = await _mediator.Send(command);

        if (result)
        {
            _responseModel.IsSuccess = true;
            return _responseModel;
        }
        _responseModel.IsSuccess = false;
        return _responseModel;
    }

    public async Task<BaseResponseModel> GetFriendshipList(UserFriendListCommand command)
    {
        var result = await _mediator.Send(command);

        _responseModel.IsSuccess = true;
        _responseModel.Data = result;
        return _responseModel;
    }
}
