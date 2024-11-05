using BlazorWebRtc.Application.DTO.UserFriend;
using BlazorWebRtc.Domain;
using BlazorWebRtc.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc.Application.Features.Queries.UserFriend;

public class UserFriendListQuery : IRequestHandler<UserFriendListCommand, List<UserFriendDto>>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;
    private Guid userId;

    public UserFriendListQuery(AppDbContext context, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task<List<UserFriendDto>> Handle(UserFriendListCommand request, CancellationToken cancellationToken)
    {
        userId = Guid.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var friends= await _context.UserFriends.Include(x=>x.ReceiverUser).Include(x => x.Requester).Where(x=>x.RequesterId==userId || x.ReceiverUserId==userId).ToListAsync();

        List<UserFriendDto> userFriendList = new();
        foreach (var friend in friends)
        {
            UserFriendDto userFriendDto = new UserFriendDto();
            if (userId==friend.RequesterId)
            {
                userFriendDto.UserId = friend.ReceiverUserId;
                userFriendDto.UserName = friend.ReceiverUser.UserName;
                userFriendDto.ProfilePicture= friend.ReceiverUser.ProfilePicture;
                userFriendDto.Email = friend.ReceiverUser.Email;
            }
            if (userId == friend.ReceiverUserId)
            {
                userFriendDto.UserId = friend.RequesterId;
                userFriendDto.UserName = friend.Requester.UserName;
                userFriendDto.ProfilePicture = friend.Requester.ProfilePicture;
                userFriendDto.Email = friend.Requester.Email;
            }
            userFriendList.Add(userFriendDto);
        }
        return userFriendList;

    }
}
