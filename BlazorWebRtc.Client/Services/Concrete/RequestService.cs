using BlazorWebRtc.Client.Models.Request;
using BlazorWebRtc.Client.Models.Response;
using BlazorWebRtc.Client.Services.Abstract;
using Newtonsoft.Json;
using System.Text;

namespace BlazorWebRtc.Client.Services.Concrete;

public class RequestService : IRequestService
{
    private readonly HttpClient _httpClient;

    public RequestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseModel> SendFriendshipRequest(RequestFriendshipCommand command)
    {
        var content = JsonConvert.SerializeObject(command);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/Request", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);

        if (response.IsSuccessStatusCode)
        {
            return result;
        }
        return new ResponseModel { IsSuccess = false };
    }
}
