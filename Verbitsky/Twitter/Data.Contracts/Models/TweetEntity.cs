using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Contracts.Models
{
    public class TweetEntity
    {
        public int Id { get; set; }
        public string Head { get; set; }
        public string Content { get; set; }
        public UserEntity Author { get; set; }
    }
}
