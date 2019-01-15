using System;
using System.Collections.Generic;
using System.Text;

namespace ItNews.Domain.Contracts.ViewModels
{
    public class PostViewModel
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

        public ShortProfileViewModel Author
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

        public int Rating
        {
            get;
            set;
        }

        public CategoryViewModel Category
        {
            get;
            set;
        }

        public IEnumerable<CommentViewModel> Comments
        {
            get;
            set;
        }

        public IEnumerable<TagViewModel> Tags
        {
            get;
            set;
        }
    }
}
