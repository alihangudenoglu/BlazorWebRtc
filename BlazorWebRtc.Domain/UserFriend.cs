using BlazorWebRtc.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebRtc.Domain;

public class UserFriend : BaseEntity
{
    [ForeignKey(nameof(Requester))]
    public Guid RequesterId { get; set; }
    [ForeignKey(nameof(ReceiverUser))]
    public Guid ReceiverUserId { get; set; }

    public User Requester { get; set; }
    public User ReceiverUser { get; set; }

    //public virtual List<User> Users { get; set; }
}
