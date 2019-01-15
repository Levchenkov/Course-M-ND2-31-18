using System;

namespace ItNews.Domain.Contracts.ViewModels
{
    public class CommentViewModel
    {
        public long Id
        {
            get;
            set;
        }

        public DateTime Created
        {
            get;
            set;
        }

        public ProfileViewModel Author
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public int Likes
        {
            get;
            set;
        }
    }
}