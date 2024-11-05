using BlazorWebRtc.Application.Features.Commands.Upload;
using BlazorWebRtc.Application.Models;

namespace BlazorWebRtc.Application.Interface.Services;

public interface IUploadService
{
    Task<BaseResponseModel> UploadFile(UploadCommand command);
}
