using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter.Data.Contracts.Entities
{
    public class TwitTag
    {
        public int TwitId { get; set; }
        public virtual Twit Twit { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag {get; set;}
    }
}
