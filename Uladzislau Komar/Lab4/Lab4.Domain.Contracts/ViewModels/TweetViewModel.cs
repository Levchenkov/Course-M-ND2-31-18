using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4.Domain.Contracts.ViewModels
{
    public class TweetViewModel
    {
        public int TweetId { get; set; }
        
        public string Content { get; set; }
        
        public DateTime Created { get; set; }

        public int AuthorId { get; set; }
        public PersonViewModel Author { get; set; }
    }
}
