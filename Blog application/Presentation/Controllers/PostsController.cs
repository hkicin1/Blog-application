using Application.Posts.Commands.CreatePost;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Pagination;
using Application.Likes.Commands.AddPostLike;
using Application.Likes.Commands.RemovePostLike;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetPost;
using Presentation.Inputs;

namespace Presentation.Controllers
{
    public class PostsController : BaseController
    {
        /// <summary>
        /// Register new post to system
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NoContentResult> CreatePost(CreatePostCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PagedResult<GetAllPostsDto>>> GetAllPosts([FromQuery] int page, int pageSize)
        {
            var response = await Mediator.Send(new GetAllPostsQuery()
            {
                 Page = page,
                 PageSize = pageSize
            });
            return Ok(response);
        }

        /// <summary>
        /// Get post by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPostByIdDto>> GetPostById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetPostByIdQuery() {Id = id});
            return Ok(response);
        }

        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="input"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<NoContentResult> UpdatePost([FromRoute] int id, UpdatePostInput input)
        {
            await Mediator.Send(new UpdatePostCommand()
            {
                Title = input.Title,
                Body = input.Body,
                Id = id
            });
            return NoContent();
        }

        /// <summary>
        /// Delete post by id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<NoContentResult> DeletePost([FromRoute] int id)
        {
            await Mediator.Send(new DeletePostCommand() {Id = id});
            return NoContent();
        }
        
        /// <summary>
        /// Add like to post with id
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/like")]
        public async Task<NoContentResult> AddPostLike([FromRoute] int id)
        {
            await Mediator.Send(new AddPostLikeCommand() { Id = id });
            return NoContent();
        }
        
        /// <summary>
        /// Remove like from post with id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("like/{id}")]
        public async Task<NoContentResult> RemovePostLike([FromRoute] int id)
        {
            await Mediator.Send(new RemovePostLikeCommand() { Id = id });
            return NoContent();
        }
    }
}