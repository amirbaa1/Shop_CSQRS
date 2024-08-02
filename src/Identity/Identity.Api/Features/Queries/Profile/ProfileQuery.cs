using Identity.Domain.Models.Dto;
using MediatR;

namespace Identity.Api.Features.Queries.Profile;

public class ProfileQuery : IRequest<ResponseDto>
{
    public string Id { get; set; }

    public ProfileQuery(string id)
    {
        Id = id;
    }
}