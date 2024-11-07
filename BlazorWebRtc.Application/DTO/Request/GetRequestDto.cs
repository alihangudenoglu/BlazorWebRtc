namespace BlazorWebRtc.Application.DTO.Request;

public class GetRequestDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? ProfilePicture { get; set; }
}
