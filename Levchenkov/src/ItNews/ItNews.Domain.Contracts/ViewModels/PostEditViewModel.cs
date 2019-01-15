namespace ItNews.Domain.Contracts.ViewModels
{
    public class PostEditViewModel
    {
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