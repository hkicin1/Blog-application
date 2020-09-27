using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Authentication.Commands.UserLogin
{
    public class UserLoginCommand : IRequest<UserLoginDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoginDto>
        {
            private readonly IIdentityService _identityService;
            private readonly ILogger<UserLoginCommandHandler> _logger;

            public UserLoginCommandHandler(IIdentityService identityService, ILogger<UserLoginCommandHandler> logger)
            {
                _identityService = identityService;
                _logger = logger;
            }

            public async Task<UserLoginDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
            {
                var response = await _identityService.LoginAsync(request.Email, request.Password);
                _logger.LogInformation("User with e-mail {Mail} logged in at {Time}", request.Email, DateTime.UtcNow);
                return new UserLoginDto(){Token = response};
            }
        }
    }
}
