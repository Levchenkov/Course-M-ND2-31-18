namespace ItNews.Data.Contracts
{
    public class Like
    {
        public long Id
        {
            get;
            set;
        }

        public long CommentId
        {
            get;
            set;
        }

        public long AuthorProfileId
        {
            get;
            set;
        }
    }
}