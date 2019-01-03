using System;

namespace Twitter.Data.Contracts.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public virtual string AuthorId { get; set; }
        public virtual User Author { get; set; }

    }
}