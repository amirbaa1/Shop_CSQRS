
using Identity.Domain.Models.Dto;
using Identity.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Features.Commands.AssignRole
{
    public class AssignRoleHandler : IRequestHandler<AssignRoleCommand, ResponseDto>
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AssignRoleHandler> _logger;

        public AssignRoleHandler(IAuthRepository authRepository, ILogger<AssignRoleHandler> logger)
        {
            _authRepository = authRepository;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authRepository.AssignRole(request.Email, request.RoleName);

            return result;
        }
    }
}
