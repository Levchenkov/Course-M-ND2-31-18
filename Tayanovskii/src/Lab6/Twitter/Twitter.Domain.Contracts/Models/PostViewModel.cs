using System;
using Twitter.Data.Contracts.Entities;

namespace Twitter.Domain.Contracts.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public virtual string AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}