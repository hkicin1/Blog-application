using System;

namespace Domain.Entities
{
    public class PostLike
    {
        public PostLike(int postId, Guid userId)
        {
            PostId = postId;
            UserId = userId;
        }

        public int Id { get; private set; }
        public int PostId { get; private set; }
        public virtual Post Post { get; set; }
        public Guid UserId { get; private set; }
    }
}
