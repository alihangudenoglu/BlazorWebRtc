using BlazorWebRtc.Client.Models.Response;
using BlazorWebRtc.Client.Services.Abstract;
using System.Net.Http.Json;

namespace BlazorWebRtc.Client.Services.Concrete;

public class UploadService : IUploadService
{
    private readonly HttpClient _httpClient;

    public UploadService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseModel> UploadFileAsync(MultipartFormDataContent content)
    {
        var response = await _httpClient.PostAsync("api/Upload",content);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ResponseModel>();
            return result;
        }

        return new ResponseModel { IsSuccess = false, Message = "File upload failed" };
    }
}
