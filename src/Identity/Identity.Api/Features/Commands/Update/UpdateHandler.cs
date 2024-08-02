using AutoMapper;
using Identity.Domain.Models.Dto;
using Identity.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Identity.Api.Features.Commands.Update;

public class UpdateHandler : IRequestHandler<UpdateCommand, ResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateHandler> _logger;
    private readonly IAuthRepository _auth;

    public UpdateHandler(IMapper mapper, ILogger<UpdateHandler> logger, IAuthRepository auth)
    {
        _mapper = mapper;
        _logger = logger;
        _auth = auth;
    }

    public async Task<ResponseDto> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var update = await _auth.UpdateProfile(request.Id, request);
        return update;
    }
}