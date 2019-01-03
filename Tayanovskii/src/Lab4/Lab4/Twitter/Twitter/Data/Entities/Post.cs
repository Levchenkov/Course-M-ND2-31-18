using System;
using Twitter.Models;

namespace Twitter.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public virtual string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

    }
}