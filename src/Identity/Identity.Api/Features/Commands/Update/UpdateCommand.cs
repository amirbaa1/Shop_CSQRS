using Identity.Domain.Models;
using Identity.Domain.Models.Dto;
using MediatR;

namespace Identity.Api.Features.Commands.Update;

public class UpdateCommand : UpdateProfile, IRequest<ResponseDto>
{
    public string Id { get; set; }

    public UpdateCommand(string id)
    {
        Id = id;
    }
}