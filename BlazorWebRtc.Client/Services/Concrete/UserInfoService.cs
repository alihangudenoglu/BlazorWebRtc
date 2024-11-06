using Blazored.LocalStorage;
using BlazorWebRtc.Client.Models;
using BlazorWebRtc.Client.Models.Response;
using BlazorWebRtc.Client.Services.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorWebRtc.Client.Services.Concrete;

public class UserInfoService : IUserInfoService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public UserInfoService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    public async Task<List<UserDtoResponseModel>> GetUserList()
    {
        var token = await _localStorageService.GetItemAsync<string>(Constants.LocalToken);

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

        var response = await _httpClient.GetAsync("api/UserInfo");
        var content= await response.Content.ReadAsStringAsync();
        var deserialize=JsonConvert.DeserializeObject<ResponseModel>(content);
        if (response.IsSuccessStatusCode)
        {
            var desObj= JsonConvert.DeserializeObject<List<UserDtoResponseModel>>(deserialize.Data.ToString());
            return desObj;
        }
        return new List<UserDtoResponseModel>();
    }
}
