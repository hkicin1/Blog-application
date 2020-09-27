using System;

namespace Application.Posts.Queries.GetPost
{
    public class GetPostByIdResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public Guid UserId { get; set; }
    }
}