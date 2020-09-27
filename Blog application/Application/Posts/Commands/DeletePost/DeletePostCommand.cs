using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Posts.Commands.DeletePost
{
    public class DeletePostCommand : IRequest
    {
        public int Id { get; set; }
        
        public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
        {
            private readonly IPostsRepository _postsRepository;
            private readonly ILogger<DeletePostCommandHandler> _logger;
            public DeletePostCommandHandler(IPostsRepository postsRepository, ILogger<DeletePostCommandHandler> logger)
            {
                _postsRepository = postsRepository;
                _logger = logger;
            }
            
            public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
            {
                await _postsRepository.DeletePost(request.Id);
                _logger.LogInformation("DELETE post {id} at {Time}", request.Id, DateTime.UtcNow);
                return Unit.Value;
            }
        }
    }
}