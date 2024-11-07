using Blazored.LocalStorage;
using BlazorWebRtc.Client;
using BlazorWebRtc.Client.Extension;
using BlazorWebRtc.Client.Services.Abstract;
using BlazorWebRtc.Client.Services.Concrete;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUserFriendService, UserFriendService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7151/") });

builder.Services.AddScoped<AuthenticationStateProvider, CustomStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped( sp =>
{
    return new HubConnectionBuilder().WithUrl($"https://localhost:7151/userhub", opt =>
    {
        opt.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
    }).Build();
});


await builder.Build().RunAsync();
