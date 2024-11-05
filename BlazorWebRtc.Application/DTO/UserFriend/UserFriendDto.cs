using BlazorWebRtc.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebRtc.Application.DTO.UserFriend;

public class UserFriendDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? ProfilePicture { get; set; }
}
