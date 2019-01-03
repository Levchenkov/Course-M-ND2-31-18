using System;

namespace Lab4.Domain.Contracts.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public User User { get; set; }
    }
}
