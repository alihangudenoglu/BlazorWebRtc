using Blazored.LocalStorage;
using BlazorWebRtc.Client.Extension;
using BlazorWebRtc.Client.Models;
using BlazorWebRtc.Client.Models.Request;
using BlazorWebRtc.Client.Models.Response;
using BlazorWebRtc.Client.Services.Abstract;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Text;

namespace BlazorWebRtc.Client.Services.Concrete;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AccountService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> Login(LoginCommand command)
    {
        var content = JsonConvert.SerializeObject(command);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/User/Login", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);

        if (response.IsSuccessStatusCode)
        {
            await _localStorageService.SetItemAsync(Constants.LocalToken, result.Data.ToString());
            ((CustomStateProvider)_authenticationStateProvider).NotifyUserLoggedIn(result.Data.ToString());

            return result.IsSuccess;
        }
        return result.IsSuccess;
    }

    public async Task Logout()
    {
        await _localStorageService.RemoveItemAsync(Constants.LocalToken);
        ((CustomStateProvider)_authenticationStateProvider).NotifyUserLogout();

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<UserResponseModel> SignUp(RegisterCommand command)
    {
        var content = JsonConvert.SerializeObject(command);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/User/Register", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);

        var user = JsonConvert.DeserializeObject<UserResponseModel>(result.Data.ToString());

        if (response.IsSuccessStatusCode)
        {            
            return user;
        }
        return null;
    }

    //public async Task<UserResponseModel> SignUp(RegisterCommand command)
    //{
    //    var content = JsonConvert.SerializeObject(command);
    //    var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

    //    var response = await _httpClient.PostAsync("api/User/Register", bodyContent);
    //    var contentTemp = await response.Content.ReadAsStringAsync();

    //    var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);

    //    if (response.IsSuccessStatusCode && result.IsSuccess && result.Data != null)
    //    {
    //        // Eğer `result.Data` yalnızca bir `GUID` içeren `string` ise
    //        var user = new UserResponseModel { Id = result.Data.ToString() };
    //        return user;
    //    }
    //    return null;
    //}


}
