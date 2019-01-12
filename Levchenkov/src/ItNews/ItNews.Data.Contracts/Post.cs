using System;

namespace ItNews.Data.Contracts
{
    public class Post
    {
        public long Id
        {
            get;
            set;
        }

        public long AuthorProfileId
        {
            get;
            set;
        }

        public DateTime Created
        {
            get;
            set;
        }

        public DateTime Updated
        {
            get;
            set;
        }

        public PostState PostState
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public long CategoryId
        {
            get;
            set;
        }

    }
}