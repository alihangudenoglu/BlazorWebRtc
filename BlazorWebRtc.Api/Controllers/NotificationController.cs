using BlazorWebRtc.Application.Hubs;
using BlazorWebRtc.Application.Interface.Services.Manager;
using BlazorWebRtc.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BlazorWebRtc.Api.Controllers
{
    public class NotificationModel
    {
        public string CallerUserId { get; set; }
        public string AnswerUserId { get; set; }
    }
    public class UserResponseModel
    {
        public string image { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<UserHub> _hubContext;
        private readonly IConnectionManager _connectionManager;
        private readonly AppDbContext _appDbContext;

        public NotificationController(IHubContext<UserHub> hubContext, IConnectionManager connectionManager, AppDbContext appDbContext)
        {
            _hubContext = hubContext;
            _connectionManager = connectionManager;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task NotificationTrigger(NotificationModel notify)
        {
            var userIds = _connectionManager.GetConnectionByUserIdSingleObj(notify.AnswerUserId);
            if (!string.IsNullOrEmpty(userIds))
            {
                var result = await _appDbContext.Users.FirstOrDefaultAsync(x=>x.Id==Guid.Parse(notify.CallerUserId));

                UserResponseModel responseModel = new UserResponseModel();
                responseModel.image = result.ProfilePicture;
                responseModel.userName = result.UserName;
                responseModel.userId = result.Id.ToString();

                var serializeMessage = JsonConvert.SerializeObject(responseModel);

                await _hubContext.Clients.Clients(userIds).SendAsync("Notify", serializeMessage);
            }

        }
    }
}


