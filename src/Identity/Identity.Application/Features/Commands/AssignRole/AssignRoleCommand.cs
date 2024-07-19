using Identity.Domain.Models.Dto;
using MediatR;


namespace Identity.Application.Features.Commands.AssignRole
{
    public class AssignRoleCommand : IRequest<ResponseDto>
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
