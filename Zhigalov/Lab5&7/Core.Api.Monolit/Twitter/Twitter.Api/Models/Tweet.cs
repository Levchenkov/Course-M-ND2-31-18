using System;

namespace Twitter.Api.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public User Author { get; set; }
    }
}