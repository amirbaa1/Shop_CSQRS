using Identity.Api.Features.Commands.AssignRole;
using Identity.Api.Features.Commands.Login;
using Identity.Api.Features.Commands.Register;
using Identity.Api.Features.Commands.Update;
using Identity.Api.Features.Queries.Profile;
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
        var response = await _mediator.Send(loginModel);
        return Ok(response);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
    {
        var response = await _mediator.Send(registerCommand);
        return Ok(response);
    }

    [HttpGet("profile/{id}")]
    public async Task<IActionResult> Profile(string id)
    {
        var response = await _mediator.Send(new ProfileQuery(id));
        return Ok(response);
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
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("AssignRole")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }


}