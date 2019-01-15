namespace ItNews.Domain.Contracts
{
    public interface IRatingService
    {
        int GetRating(long postId);
    }
}