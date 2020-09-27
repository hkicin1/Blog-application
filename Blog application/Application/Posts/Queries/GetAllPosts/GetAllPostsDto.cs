using System;

namespace Application.Posts.Queries.GetAllPosts
{
    public class GetAllPostsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public Guid UserId { get; set; }
    }
}