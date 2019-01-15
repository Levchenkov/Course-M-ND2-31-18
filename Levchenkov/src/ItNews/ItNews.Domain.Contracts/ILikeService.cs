namespace ItNews.Domain.Contracts
{
    public interface ILikeService
    {
        int GetLikes(long commentId);
    }
}