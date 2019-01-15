namespace ItNews.Data.Contracts.Entities
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