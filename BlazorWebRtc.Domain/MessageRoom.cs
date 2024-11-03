using BlazorWebRtc.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebRtc.Domain;

public class MessageRoom:BaseEntity
{
    [ForeignKey(nameof(SenderUser))]
    public Guid? SenderUserId { get; set; }
    [ForeignKey(nameof(ReceiverUser))]
    public Guid ReceiverUserId { get; set; }

    public User SenderUser { get; set; }
    public User ReceiverUser { get; set; }

    public virtual ICollection<Message> Messages { get; set; }
}
