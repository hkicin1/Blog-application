using System;

namespace Domain.Entities
{
    public class PostLike
    {
        public int PostId { get; set; }

        public Guid UserId { get; set; }
    }
}