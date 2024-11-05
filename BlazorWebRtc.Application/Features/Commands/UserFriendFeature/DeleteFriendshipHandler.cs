using BlazorWebRtc.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc.Application.Features.Commands.Feature;

public class DeleteFriendshipHandler : IRequestHandler<DeleteFriendshipCommand, bool>
{
    private readonly AppDbContext _context;

    public DeleteFriendshipHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteFriendshipCommand request, CancellationToken cancellationToken)
    {
        var userFriend = await _context.UserFriends.FirstOrDefaultAsync(x => x.Id == request.Id);

        _context.UserFriends.Remove(userFriend);

        if (await _context.SaveChangesAsync(cancellationToken) > 0)
        {
            return true;
        }
        return false;
    }
}
