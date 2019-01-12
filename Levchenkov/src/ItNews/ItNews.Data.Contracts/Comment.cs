using System;

namespace ItNews.Data.Contracts
{
    public class Comment
    {
        public long Id
        {
            get;
            set;
        }

        public long PostId
        {
            get;
            set;
        }

        public DateTime Created
        {
            get;
            set;
        }

        public long AuthorProfileId
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }
    }
}