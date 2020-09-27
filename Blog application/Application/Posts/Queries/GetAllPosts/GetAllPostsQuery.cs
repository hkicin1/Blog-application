using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Pagination;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Posts.Queries.GetAllPosts
{
    public class GetAllPostsQuery : IRequest<PagedResult<GetAllPostsDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PagedResult<GetAllPostsDto>>
        {
            private readonly IPostsRepository _postsRepository;
            private readonly ILogger<GetAllPostsQueryHandler> _logger;
            public GetAllPostsQueryHandler(IPostsRepository postsRepository,
                ILogger<GetAllPostsQueryHandler> logger)
            {
                _postsRepository = postsRepository;
                _logger = logger;
            }
            
            public async Task<PagedResult<GetAllPostsDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
            {
                var posts = await _postsRepository.GetAllPosts(request.Page, request.PageSize);
                _logger.LogInformation("GET all posts requested at {Time}", DateTime.UtcNow);
                return posts;
            }
        }

    }
}