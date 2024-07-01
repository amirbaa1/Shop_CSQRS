using Identity.Application.Features.Commands.Login;
using Identity.Application.Features.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginModel)
    {
        var request = await _mediator.Send(loginModel);
        return Ok(request);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
    {
        var request = await _mediator.Send(registerCommand);
        return Ok(request);
    }
}