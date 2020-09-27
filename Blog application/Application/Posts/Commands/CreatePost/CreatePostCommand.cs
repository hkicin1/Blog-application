using System;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest
    {
        public string Title { get; set; }

        public string Body { get; set; }
        
        public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
        {
            private readonly IPostsRepository _repository;
            private readonly IMapper _mapper;
            private readonly ICurrentUserService _userService;
            private readonly ILogger<CreatePostCommandHandler> _logger;

            public CreatePostCommandHandler(IPostsRepository repository, IMapper mapper, 
                ICurrentUserService userService, ILogger<CreatePostCommandHandler> logger)
            {
                _repository = repository;
                _mapper = mapper;
                _userService = userService;
                _logger = logger;
            }

            public async Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                await _repository.CreatePost(_mapper.Map<CreatePostRequest>(request), _userService.UserId);
                _logger.LogInformation("New post created at {Time}", DateTime.UtcNow);
                return Unit.Value;
            }
        }

    }
}
