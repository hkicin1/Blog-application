using System;
using Application.Posts.Commands.CreatePost;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Pagination;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetPost;

namespace Application.Interfaces
{
    public interface IPostsRepository
    {
        Task CreatePost(CreatePostRequest request, Guid userId);
        Task<GetPostByIdDto> GetPostById(int id);
        Task<PagedResult<GetAllPostsDto>> GetAllPosts(int page, int pageSize);
        Task UpdatePost(UpdatePostRequest request);
        Task DeletePost(int id);
    }
}
