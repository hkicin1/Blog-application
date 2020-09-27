using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public int Id { get; set; }
        
        public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
        {
            private readonly IPostsRepository _repository;
            private readonly IMapper _mapper;
            private readonly ILogger<UpdatePostCommandHandler> _logger;
            public UpdatePostCommandHandler(IPostsRepository repository, IMapper mapper,
                ILogger<UpdatePostCommandHandler> logger)
            {
                _repository = repository;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
            {
                await _repository.UpdatePost(_mapper.Map<UpdatePostRequest>(request));
                _logger.LogInformation("Post {title} updated at {Time}", request.Title, DateTime.UtcNow);
                return Unit.Value;
            }
        }
    }
}