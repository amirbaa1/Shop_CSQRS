using Identity.Domain.Models;
using Identity.Domain.Models.Dto;
using MediatR;

namespace Identity.Application.Features.Commands.Login;

public class LoginCommand : IRequest<LoginResponseDto>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}