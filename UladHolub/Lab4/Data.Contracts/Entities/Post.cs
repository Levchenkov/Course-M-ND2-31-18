using System;

namespace Data.Contracts.Entities
{
    public class Post
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}
