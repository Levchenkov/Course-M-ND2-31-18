using System;

namespace ItNews.Domain.Contracts.ViewModels
{
    public class PostTableViewModel
    {
        public long Id
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
    }
}