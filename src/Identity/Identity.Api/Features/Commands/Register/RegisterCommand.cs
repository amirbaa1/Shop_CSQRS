using Identity.Domain.Models;
using MediatR;

namespace Identity.Api.Features.Commands.Register;

public class RegisterCommand : IRequest<string>
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}