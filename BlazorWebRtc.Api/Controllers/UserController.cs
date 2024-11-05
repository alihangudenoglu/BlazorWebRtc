using BlazorWebRtc.Application.Features.Commands.Account.Login;
using BlazorWebRtc.Application.Features.Commands.Account.Register;
using BlazorWebRtc.Application.Interface.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly IAccountService _accountService;

    public UserController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        return Ok(await _accountService.SignUp(command));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        return Ok(await _accountService.SignIn(command));
    }

}
