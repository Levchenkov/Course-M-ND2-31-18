using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter.Data.Contracts.Entities
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TwitTag> TwitTags { get; set; }
    }
}
