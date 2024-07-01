using AutoMapper;
using Identity.Domain.Models;
using Identity.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Identity.Application.Features.Commands.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly ILogger<RegisterHandler> _logger;
    private readonly IAuthRepository _auth;
    private readonly IMapper _mapper;

    public RegisterHandler(ILogger<RegisterHandler> logger, IAuthRepository auth, IMapper mapper)
    {
        _logger = logger;
        _auth = auth;
        _mapper = mapper;
    }
    public Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userMap = _mapper.Map<RegisterModel>(request);
        _logger.LogInformation($"Register:{JsonConvert.SerializeObject(userMap)}");
        var register = _auth.Register(userMap);
        return register;
    }
}