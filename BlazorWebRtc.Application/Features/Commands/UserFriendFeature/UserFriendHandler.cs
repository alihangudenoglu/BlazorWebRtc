using BlazorWebRtc.Domain;
using BlazorWebRtc.Persistence.Context;
using MediatR;

namespace BlazorWebRtc.Application.Features.Commands.Feature;

public class UserFriendHandler : IRequestHandler<UserFriendCommand, bool>
{
    private readonly AppDbContext _context;

    public UserFriendHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UserFriendCommand request, CancellationToken cancellationToken)
    {
        UserFriend userFriend = new UserFriend();

        userFriend.RequesterId = request.RequesterId;
        userFriend.ReceiverUserId = request.ReceiverUserId;

        await _context.UserFriends.AddAsync(userFriend, cancellationToken);

        if (await _context.SaveChangesAsync(cancellationToken) > 0)
        {
            return true;
        }
        return false;
    }
}
