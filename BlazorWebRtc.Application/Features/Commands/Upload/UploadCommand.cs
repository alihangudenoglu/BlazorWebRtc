using MediatR;

namespace BlazorWebRtc.Application.Features.Commands.Upload;

public class UploadCommand:IRequest<bool>
{
    public string File { get; set; }
    public Guid UserId { get; set; }
}
