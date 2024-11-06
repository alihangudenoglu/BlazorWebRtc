namespace BlazorWebRtc.Client.Models.Response;

public class LoginResponseModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? ProfilePicture { get; set; }
}
