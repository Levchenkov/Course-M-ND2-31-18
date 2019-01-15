using ItNews.Domain.Contracts.ViewModels;

namespace ItNews.Domain.Contracts
{
    public interface IPostService
    {
        PostViewModel GetPost(long postId);
    }
}
