using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Post
    {
        public Post(string title, string body, Guid userId)
        {
            Title = title;
            Body = body;
            UserId = userId;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Body { get; private set; }

        public Guid UserId { get; private set; }

        public IEnumerable<PostLike> Likes { get; private set; }

        public void Update(string requestTitle, string requestBody)
        {
            if (!string.IsNullOrEmpty(requestTitle))
                Title = requestTitle;

            if (!string.IsNullOrEmpty(requestBody))
                Body = requestBody;
        }
    }
}
