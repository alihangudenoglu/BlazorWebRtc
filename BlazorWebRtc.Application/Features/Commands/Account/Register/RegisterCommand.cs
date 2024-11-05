using BlazorWebRtc.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc.Application.Features.Commands.Account.Register;

public class RegisterCommand:IRequest<User>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}
