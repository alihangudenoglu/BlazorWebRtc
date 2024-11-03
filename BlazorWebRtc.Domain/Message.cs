using BlazorWebRtc.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebRtc.Domain;

public class Message : BaseEntity
{
    public string MessageContent { get; set; }

    [ForeignKey(nameof(MessageRoom))]
    public Guid MessageRoomId { get; set; }
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public MessageRoom MessageRoom { get; set; }
    public User User { get; set; }

}
