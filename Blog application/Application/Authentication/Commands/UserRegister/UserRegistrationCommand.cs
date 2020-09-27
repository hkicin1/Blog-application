using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Authentication.Commands.UserRegister
{
    public class UserRegistrationCommand : IRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand>
        {
            private readonly IIdentityService _identityService;
            private readonly ILogger<UserRegistrationCommandHandler> _logger;

            public UserRegistrationCommandHandler(IIdentityService identityService, ILogger<UserRegistrationCommandHandler> logger)
            {
                _identityService = identityService;
                _logger = logger;
            }

            public async Task<Unit> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
            {
                await _identityService.RegisterAsync(request.Email, request.UserName, request.Password);
                _logger.LogInformation("New user registered at {Time}", DateTime.UtcNow);
                return Unit.Value;
            }
        }
    }
}
