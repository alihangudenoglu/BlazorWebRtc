namespace BlazorWebRtc.Client.Models.Response;

public class UserDtoResponseModel
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? ProfilePicture { get; set; }
    public bool IsOnline { get; set; } = false;
}
