using AutoMapper;
using Identity.Domain.Models;
using Identity.Domain.Models.Dto;
using Identity.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Identity.Application.Features.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IAuthRepository _auth;
    private readonly IMapper _mapper;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(IAuthRepository auth, IMapper mapper, ILogger<LoginHandler> logger)
    {
        _auth = auth;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"1 login : {JsonConvert.SerializeObject(request)}");
        var loginMap = _mapper.Map<LoginModel>(request);
        _logger.LogInformation($"2 map login : {JsonConvert.SerializeObject(loginMap)}");
        var loginUser = await _auth.Login(loginMap);
        _logger.LogInformation($"final :login resposnse : {loginUser}");
        return loginUser;
    }
}