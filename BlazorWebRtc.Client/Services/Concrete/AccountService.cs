using BlazorWebRtc.Client.Models.Request;
using BlazorWebRtc.Client.Models.Response;
using BlazorWebRtc.Client.Services.Abstract;
using Newtonsoft.Json;
using System.Text;

namespace BlazorWebRtc.Client.Services.Concrete;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;

    public AccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
