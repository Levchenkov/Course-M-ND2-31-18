using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter.Data.Contracts.Entities
{
    public class Twit
    {
        public int TwitId { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public virtual ICollection<TwitTag> TwitTags{get; set;}
    }
}
