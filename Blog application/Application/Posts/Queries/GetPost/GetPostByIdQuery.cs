using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Posts.Queries.GetPost
{
    public class GetPostByIdQuery : IRequest<GetPostByIdDto>
    {
        public int Id { get; set; }

        public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, GetPostByIdDto>
        {
            private readonly IMapper _mapper;
            private readonly IPostsRepository _postsRepository;
            private readonly ILogger<GetPostByIdQueryHandler> _logger;
            public GetPostByIdQueryHandler(IPostsRepository postsRepository, IMapper mapper,
                ILogger<GetPostByIdQueryHandler> logger)
            {
                _postsRepository = postsRepository;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<GetPostByIdDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
            {
                var post = await _postsRepository.GetPostById(request.Id);
                _logger.LogInformation("GET post by id {id} requested at {Time}", request.Id, DateTime.UtcNow);
                return _mapper.Map<GetPostByIdDto>(post);
            }
        }
    }
}