namespace BlazorWebRtc.Client.Models.Request;

public class SendMessageModel
{
    public string MessageContent { get; set; }
    public string ReceiverUserId { get; set; }
}
