using BlazorWebRtc.Client.Extension;
using BlazorWebRtc.Client.Models.Request;
using BlazorWebRtc.Client.Models.Response;
using BlazorWebRtc.Client.Services.Abstract;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace BlazorWebRtc.Client.Services.Concrete;

public class RequestService : IRequestService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public RequestService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<List<GetRequestFriendshipList>> GetFriendshipRequest()
    {
        var result= await ((CustomStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();


        var response = await _httpClient.GetAsync($"api/Request/{result.User.FindFirst(ClaimTypes.NameIdentifier)?.Value}");
        var content = await response.Content.ReadAsStringAsync();
        var deserialize = JsonConvert.DeserializeObject<ResponseModel>(content);
        if (deserialize.IsSuccess)
        {
            var desObj = JsonConvert.DeserializeObject<List<GetRequestFriendshipList>>(deserialize.Data.ToString());
            return desObj;
        }
        return new List<GetRequestFriendshipList>();
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

    public async Task<ResponseModel> UpdateRequest(UpdateRequestModel requestModel)
    {
        var content = JsonConvert.SerializeObject(requestModel);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync("api/Request", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);

        if (response.IsSuccessStatusCode)
        {
            return result;
        }
        return new ResponseModel { IsSuccess = false };
    }
}
