using BlazorWebRtc.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc.Application.Features.Commands.Upload;

public class UploadHandler : IRequestHandler<UploadCommand, bool>
{
    private readonly AppDbContext _context;

    public UploadHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UploadCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Users.FirstOrDefaultAsync(x=>x.Id==request.UserId);
        result.ProfilePicture = request.File;

        if (await _context.SaveChangesAsync(cancellationToken)>0)
        {
            return true;
        }
        return false;
    }
}
