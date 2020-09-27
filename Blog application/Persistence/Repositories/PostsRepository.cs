using System;
using Application.Interfaces;
using Application.Posts.Commands.CreatePost;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetPost;

namespace Persistence.Repositories
{
    public class PostsRepository : IPostsRepository, ILikesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePost(CreatePostRequest request, Guid userId)
        {
            var entity = new Post(request.Title, request.Body, userId);
            await _dbContext.Posts.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePost(int postId)
        {
            var entity = await _dbContext.Posts.FindAsync(postId);

            if (entity == null) 
                throw new NotFoundException("The post you are trying to delete does not exist or you have no permission!"); 

            _dbContext.Posts.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedResult<GetAllPostsDto>> GetAllPosts(int page, int pageSize)
        {
            return await _dbContext.Posts
                .Select(p =>
                    new GetAllPostsDto()
                    {
                        Id = p.Id,
                        Body = p.Body,
                        Title = p.Title,
                        UserId = p.UserId
                    })
                .GetPagedAsync(page, pageSize);
        }

        public async Task<GetPostByIdDto> GetPostById(int id)
        {
            return await _dbContext.Posts
                .Where(p => p.Id == id)
                .Select(p => 
                    new GetPostByIdDto()
                        {
                            Id = p.Id,
                            Title = p.Title,
                            Body = p.Body,
                            UserId = p.UserId,
                            NumberOfLikes = p.Likes.Count(),
                            UsersWhoLiked = p.Likes.Select(pl => _dbContext.Users
                                    .Where(u => u.Id == pl.UserId)
                                    .Select(u => u.UserName)
                                    .FirstOrDefault())
                                .Distinct()
                                .ToList()
                        })
                .FirstOrDefaultAsync();
        }

        public async Task UpdatePost(UpdatePostRequest request)
        {
            var entity = await _dbContext.Posts.FindAsync(request.Id);
            entity.Update(request.Title, request.Body);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddLike(int postId, Guid userId)
        {
            var entity = new PostLike(postId, userId);
            await _dbContext.PostLikes.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveLike(int id, Guid userId)
        {
            var postLike = await _dbContext.PostLikes
                .FirstOrDefaultAsync(p => p.PostId == id && p.UserId == userId);
            _dbContext.PostLikes.Remove(postLike);
            await _dbContext.SaveChangesAsync();
        }
    }
}
