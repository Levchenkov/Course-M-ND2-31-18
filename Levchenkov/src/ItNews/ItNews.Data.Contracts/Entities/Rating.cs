namespace ItNews.Data.Contracts.Entities
{
    public class Rating
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

        public int Value
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