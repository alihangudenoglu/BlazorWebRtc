using BlazorWebRtc.Client.Models.Response;
using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc.Client.Services.Abstract;

public interface IUploadService
{
    Task<ResponseModel> UploadFileAsync(MultipartFormDataContent content);
}
