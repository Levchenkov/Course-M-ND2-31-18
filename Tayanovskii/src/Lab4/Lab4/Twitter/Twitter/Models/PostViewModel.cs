using System;

namespace Twitter.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public virtual string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}