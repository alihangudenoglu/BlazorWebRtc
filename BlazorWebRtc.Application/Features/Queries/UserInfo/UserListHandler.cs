using BlazorWebRtc.Application.DTO;
using BlazorWebRtc.Domain;
using BlazorWebRtc.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc.Application.Features.Queries.UserInfo;

public class UserListHandler : IRequestHandler<UserListQuery, List<UserDto>>
{
    private readonly AppDbContext _appDbContext;
    private readonly IHttpContextAccessor _contextAccessor;
    private string? userId;

    public UserListHandler(AppDbContext appDbContext, IHttpContextAccessor contextAccessor)
    {
        _appDbContext = appDbContext;
        _contextAccessor = contextAccessor;
    }

    public async Task<List<UserDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        List<User> users = await _appDbContext.Users.ToListAsync(cancellationToken);

        userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (users != null)
        {
            List<UserDto> usersDto = new List<UserDto>();
            foreach (var user in users)
            {

                if (userId is not null && userId != user.Id.ToString())
                {
                    UserDto userDto = new UserDto();
                    userDto.UserId = user.Id;
                    userDto.UserName = user.UserName;
                    userDto.Email = user.Email;
                    userDto.ProfilePicture = user.ProfilePicture;
                    usersDto.Add(userDto);
                }
                
            }
            return usersDto;
        }

        return null;

    }
}
