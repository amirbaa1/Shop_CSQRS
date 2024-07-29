using AutoMapper;
using Identity.Domain.Models.Dto;
using Identity.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Identity.Api.Features.Queries.Profile;

public class ProfileHandler : IRequestHandler<ProfileQuery, ResponseDto>
{
    private readonly ILogger<ProfileHandler> _logger;
    private readonly IAuthRepository _auth;
    private readonly IMapper _mapper;

    public ProfileHandler(ILogger<ProfileHandler> logger, IAuthRepository auth, IMapper mapper)
    {
        _logger = logger;
        _auth = auth;
        _mapper = mapper;
    }

    public async Task<ResponseDto> Handle(ProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _auth.ProfileService(request.Id);

        var map = _mapper.Map<ResponseDto>(user);

        return map;
    }
}