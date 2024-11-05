using BlazorWebRtc.Client;
using BlazorWebRtc.Client.Services.Abstract;
using BlazorWebRtc.Client.Services.Concrete;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7151/") });

await builder.Build().RunAsync();
