using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Likes.Commands.AddPostLike
{
    public class AddPostLikeCommand : IRequest
    {
        public int Id { get; set; }
        
        public class AddPostLikeCommandHandler : IRequestHandler<AddPostLikeCommand>
        {
            private readonly ILikesRepository _repository;
            private readonly ILogger<AddPostLikeCommandHandler> _logger;
            private readonly ICurrentUserService _userService;

            public AddPostLikeCommandHandler(ILikesRepository repository, ILogger<AddPostLikeCommandHandler> logger,
                ICurrentUserService userService)
            {
                _repository = repository;
                _logger = logger;
                _userService = userService;
            }

            public async Task<Unit> Handle(AddPostLikeCommand request, CancellationToken cancellationToken)
            {
                await _repository.AddLike(request.Id, _userService.UserId);
                _logger.LogInformation("New like from {user} to post {post}", _userService.Email, request.Id);
                return Unit.Value;
            }
        }
    }
}