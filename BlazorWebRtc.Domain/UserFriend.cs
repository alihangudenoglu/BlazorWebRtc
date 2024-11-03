using BlazorWebRtc.Domain.Common;

namespace BlazorWebRtc.Domain;

public class UserFriend : BaseEntity
{
    public virtual List<User> Users { get; set; }
}
