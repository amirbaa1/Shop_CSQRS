using Identity.Application.Features.Commands.Login;
using Identity.Application.Features.Commands.Register;
using Identity.Application.Features.Commands.Update;
using Identity.Application.Features.Queries.Profile;
using Identity.Domain.Models;
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

    [HttpGet("profile/{id}")]
    public async Task<IActionResult> Profile(string id)
    {
        var request = await _mediator.Send(new ProfileQuery(id));
        return Ok(request);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfile update, [FromQuery] string id)
    {
        var command = new UpdateCommand(id)
        {
            FirstName = update.FirstName,
            LastName = update.LastName,
            Id = id,
            Image = update.Image
        };
        var request = await _mediator.Send(command);
        return Ok(request);
    }
}