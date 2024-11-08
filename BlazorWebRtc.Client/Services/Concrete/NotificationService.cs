using BlazorWebRtc.Client.Models.Request;
using BlazorWebRtc.Client.Models.Response;
using BlazorWebRtc.Client.Services.Abstract;
using Newtonsoft.Json;
using System.Text;

namespace BlazorWebRtc.Client.Services.Concrete;

public class NotificationService : INotificationService
{
    private readonly HttpClient _httpClient;

    public NotificationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<NotificationResponseModel> SendNotification(NotificationRequestModel requestModel)
    {
        var content = JsonConvert.SerializeObject(requestModel);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/Notification", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<NotificationResponseModel>(contentTemp);

        if (response.IsSuccessStatusCode)
        {
            return result;
        }
        return new NotificationResponseModel();
    }
}
