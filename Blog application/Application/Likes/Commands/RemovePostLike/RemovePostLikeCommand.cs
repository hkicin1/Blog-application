using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Likes.Commands.RemovePostLike
{
    public class RemovePostLikeCommand : IRequest
    {
        public int Id { get; set; }
        public class RemovePostLikeCommandHandler : IRequestHandler<RemovePostLikeCommand>
        {
            private readonly ILikesRepository _repository;
            private readonly ICurrentUserService _currentUser;
            private readonly ILogger<RemovePostLikeCommandHandler> _logger;
            
            public RemovePostLikeCommandHandler(ILikesRepository repository, ICurrentUserService currentUser, ILogger<RemovePostLikeCommandHandler> logger)
            {
                _repository = repository;
                _currentUser = currentUser;
                _logger = logger;
            }

            public async Task<Unit> Handle(RemovePostLikeCommand request, CancellationToken cancellationToken)
            {
                await _repository.RemoveLike(request.Id, _currentUser.UserId);
                _logger.LogInformation("Post like {id} removed.", request.Id);
                return Unit.Value;
            }
        }

    }
}