namespace BlazorWebRtc.Application.DTO.Message;

public class MessageDto
{
    public DateTime CreateDate { get; set; }
    public string MessageContent { get; set; }
    public Guid? SenderUserId { get; set; }
    public Guid? ReceiverUserId { get; set; }
}
