using System;
using System.Collections.Generic;

namespace Application.Posts.Queries.GetPost
{
    public class GetPostByIdDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public int NumberOfLikes { get; set; }
        public IList<string> UsersWhoLiked { get; set; }
    }
}